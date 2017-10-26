using System;
using System.Collections.Generic;

namespace BowlingApp.Test
{
	internal class Game
	{
		private static readonly int MAX_FRAME_COUNT = 10;

		private int score { get; set; }
		private List<Frame> frames { get; set; }
		public bool IsFinished
		{
			get
			{
				return frames.Count == 10 && frames.TrueForAll(en => en.IsFinished);
			}
		}

		public Game()
		{
			frames = new List<Frame>();
		}

		public Frame CreateFrame()
		{
			Frame frame;
			if (IsFinished == false)
			{
				frame = new Frame(frames.Count + 1, frames.Count+1 == MAX_FRAME_COUNT);
				AddFrame(frame);
			}
			else
			{
				throw new Exception("Game is finished. New frame can not be create.");
			}
			return frame;
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
		}
	}
}