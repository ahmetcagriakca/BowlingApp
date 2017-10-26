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
		public bool IsFinished
		{
			get
			{
				if (IsLastGame)
				{
					if (AllPinsAreKnocked == false && Rolls.Count == 2)
						return true;
					else if (AllPinsAreKnocked == true && Rolls.Count == 3)
						return true;
					else
						return false;
				}
				else
				{
					return AllPinsAreKnocked == true || Rolls.Count == 2;
				}
			}
		}

		public Frame NextFrame;
		private int FrameNumber;
		private bool IsLastGame;

		public Frame()
		{
			pinCount = 0;
			Rolls = new List<Roll>();
		}

		public Frame(int frameNumber) : this()
		{
			this.FrameNumber = frameNumber;
		}

		public Frame(int frameNumber, bool isLastGame) : this(frameNumber)
		{
			this.IsLastGame = isLastGame;
		}

		internal void DoRoll(int knockedPin)
		{
			Roll roll = new Roll();
			roll.Pins = knockedPin;
			Rolls.Add(roll);
			if (!IsLastGame)
			{
				pinCount += knockedPin;
				if (pinCount == DEFAULT_PIN_COUNT)
				{
					AllPinsAreKnocked = true;
				}
			}
			else
			{
				pinCount += knockedPin;
				if (pinCount == DEFAULT_PIN_COUNT)
				{
					AllPinsAreKnocked = true;
				}

			}
		}


		private int GetFrameKnockedPinCount()
		{
			return pinCount;
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
					if (!IsLastGame)
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

									if (!NextFrame.IsLastGame)
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
									else
									{
										if (NextFrame.Rolls.Count > 1)
										{
											score = GetFrameKnockedPinCount() + NextFrame.Rolls[0].Pins + NextFrame.Rolls[1].Pins;
											FrameScoreIsCalculated = true;
											return score;
										}
									}
								}
							}
						}
						else
						{
							if (Rolls.Count > 1)
							{
								score = GetFrameKnockedPinCount();
								FrameScoreIsCalculated = true;
							}
							return score;
						}
					}
					else
					{
						if (AllPinsAreKnocked)
						{
							if (Rolls.Count == 3)
							{
								score = Rolls[0].Pins + Rolls[1].Pins + Rolls[2].Pins;
								FrameScoreIsCalculated = true;
								return score;
								/*
									if (Rolls[0].Pins != DEFAULT_PIN_COUNT && (Rolls[0].Pins + Rolls[1].Pins == DEFAULT_PIN_COUNT))
									{
										score = Rolls[0].Pins + Rolls[1].Pins + Rolls[2].Pins + Rolls[2].Pins;
										FrameScoreIsCalculated = true;
										return score;
									}
									else
									if (Rolls[0].Pins == DEFAULT_PIN_COUNT)
									{
										score = Rolls[0].Pins + Rolls[1].Pins + Rolls[2].Pins ;
										FrameScoreIsCalculated = true;
										return score;
									}*/
							}
						}
						else
						{
							score = GetFrameKnockedPinCount();
							FrameScoreIsCalculated = true;
							return score;
						}
					}
				}
			}
			return 0;
		}


		internal int GetRemainingPinCount()
		{
			return DEFAULT_PIN_COUNT - pinCount;

		}
	}
}
