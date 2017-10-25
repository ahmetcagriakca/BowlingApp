using System;
using System.Collections.Generic;

namespace BowlingApp.Test
{
	internal class Game
	{
		private int score { get; set; }
		private List<Frame> frames { get; set; } = new List<Frame>();
		public bool IsFinished { get; internal set; }

		public Game()
		{
		}

		internal int Score()
		{
			score = 0;
			foreach (var item in frames)
			{
				score += item.GetScore();
			}
			return score;
		}

		internal int GetFrameScore(int v)
		{
			if (frames.Count < v)
			{
				throw new Exception("FrameNotFound");
			}
			Frame frame = frames[v - 1];
			return frame.GetScore();
		}

		internal void AddFrame(Frame frame)
		{
			if (frames.Count > 0)
			{
				frames[frames.Count - 1].NextFrame = frame;
			}
			frames.Add(frame);
			if (frames.Count == 10)
			{
				IsFinished = true;
			}
		}
	}
}