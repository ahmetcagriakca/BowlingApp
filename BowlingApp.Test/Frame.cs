using System;
using System.Collections.Generic;

namespace BowlingApp.Test
{
	internal class Frame
	{
		private static readonly int DEFAULT_PIN_COUNT = 10;

		private int pinCount;

		private int score { get; set; } = 0;

		private List<Roll> Rolls { get; set; } = new List<Roll>();
		public bool AllPinsAreKnocked { get; private set; }
		public bool FrameScoreIsCalculated { get; private set; }

		public Frame NextFrame;

		public Frame()
		{
			pinCount = DEFAULT_PIN_COUNT;
			Rolls = new List<Roll>();
		}

		internal void DoRoll(int v)
		{
			Roll roll = new Roll();
			roll.Pins = v;
			Rolls.Add(roll);
			pinCount -= v;
			if (pinCount == 0)
			{
				AllPinsAreKnocked = true;
			}
		}


		private int GetFrameKnockedPinCount()
		{
			return DEFAULT_PIN_COUNT - GetPinCount();
		}

		internal int GetScore()
		{
			if (Rolls.Count > 0)
			{

				if (FrameScoreIsCalculated)
				{
					return score;
				}
				else
				{
					if (AllPinsAreKnocked)
					{
						if (Rolls.Count == 2)
						{
							if (NextFrame != null && NextFrame.Rolls.Count > 0)
							{
								score = GetFrameKnockedPinCount() + NextFrame.Rolls[0].Pins;
								FrameScoreIsCalculated = true;
								return score;
							}
							else
							{

							}
						}
						else if (Rolls.Count == 1)
						{
							if (NextFrame != null)
							{
								if (NextFrame.Rolls.Count == 2)
								{
									score = GetFrameKnockedPinCount() + NextFrame.Rolls[0].Pins + NextFrame.Rolls[1].Pins;
									FrameScoreIsCalculated = true;
									return score;
								}
								else if (NextFrame.Rolls.Count == 1)
								{

									if (NextFrame.NextFrame != null && NextFrame.NextFrame.Rolls.Count > 0)
									{
										score = GetFrameKnockedPinCount() + NextFrame.Rolls[0].Pins + NextFrame.NextFrame.Rolls[0].Pins;
										FrameScoreIsCalculated = true;
										return score;
									}
									else
									{

									}
								}
							}
						}
					}
					else
					{
						score = GetFrameKnockedPinCount();
						if (Rolls.Count > 1)
							FrameScoreIsCalculated = true;
						return score;
					}
					return 0;
				}
			}
			return 0;
		}


		internal int GetPinCount()
		{
			return pinCount;

		}
	}
}