using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace BowlingApp.Test
{
	[TestClass]
	public class BowlingTests
	{
		/// <summary>
		/// Scenario 1
		/// </summary>
		[TestMethod]
		public void TwoRoll()
		{
			Game game = new Game();
			Frame frame = new Frame();
			frame.DoRoll(5);
			frame.DoRoll(4);
			game.AddFrame(frame);
			Assert.AreEqual(9, game.GetFrameScore(1));
		}

		/// <summary>
		/// Scenario 2
		/// </summary>
		[TestMethod]
		public void SixRoll()
		{
			Game game = new Game();
			Frame frame = new Frame();
			frame.DoRoll(5);
			frame.DoRoll(4);
			game.AddFrame(frame);
			frame = new Frame();
			frame.DoRoll(8);
			frame.DoRoll(1);
			game.AddFrame(frame);
			frame = new Frame();
			frame.DoRoll(2);
			frame.DoRoll(4);
			game.AddFrame(frame);
			Assert.AreEqual(9, game.GetFrameScore(2));
			Assert.AreEqual(24, game.Score());
		}

		/// <summary>
		/// Scenario 3
		/// </summary>
		[TestMethod]
		public void TwentyRollKnockZeroPin()
		{
			Game game = new Game();
			for (int i = 0; i < 10; i++)
			{
				Frame frame = new Frame();
				for (int j = 0; j < 2; j++)
				{
					frame.DoRoll(0);
				}
				game.AddFrame(frame);
			}
			Assert.AreEqual(0, game.Score());
		}


		/// <summary>
		/// Scenario 4
		/// </summary>
		[TestMethod]
		public void TwentyRollKnockOnePin()
		{
			Game game = new Game();
			for (int i = 0; i < 10; i++)
			{
				Frame frame = new Frame();
				for (int j = 0; j < 2; j++)
				{
					frame.DoRoll(1);
				}
				game.AddFrame(frame);
			}
			Assert.AreEqual(20, game.Score());
		}

		/// <summary>
		/// Scenario 5
		/// </summary>
		[TestMethod]
		public void RollToEndWithoutSpareOrStrike()
		{
			int totalScore = 0;
			Random random = new Random();
			Game game = new Game();
			while (!game.IsFinished)
			{
				Frame frame = new Frame();
				int pinCount = frame.GetPinCount();
				int rollPins = random.Next(pinCount);
				totalScore += rollPins;
				frame.DoRoll(rollPins);
				pinCount = frame.GetPinCount();
				if (pinCount > 0)
				{
					rollPins = random.Next(pinCount);
					totalScore += rollPins;
					frame.DoRoll(rollPins);
				}
				game.AddFrame(frame);
			}
			Assert.AreEqual(totalScore, game.Score(), totalScore);
		}

		/// <summary>
		/// Scenario 6
		/// </summary>
		[TestMethod]
		public void SixRollWithSpare()
		{
			Game game = new Game();
			Frame frame = new Frame();
			frame.DoRoll(5);
			frame.DoRoll(5);
			game.AddFrame(frame);
			frame = new Frame();
			frame.DoRoll(8);
			frame.DoRoll(1);
			game.AddFrame(frame);
			frame = new Frame();
			frame.DoRoll(2);
			frame.DoRoll(4);
			game.AddFrame(frame);
			Assert.AreEqual(18, game.GetFrameScore(1));
			Assert.AreEqual(9, game.GetFrameScore(2));
			Assert.AreEqual(33, game.Score());
		}

		/// <summary>
		/// Scenario 7
		/// </summary>
		[TestMethod]
		public void TwentyRollWithThreeSpare()
		{
			Game game = new Game();
			Frame frame = new Frame();
			frame.DoRoll(5);
			frame.DoRoll(5);
			game.AddFrame(frame);
			frame = new Frame();
			frame.DoRoll(8);
			frame.DoRoll(1);
			game.AddFrame(frame);
			frame = new Frame();
			frame.DoRoll(2);
			frame.DoRoll(4);
			game.AddFrame(frame);
			frame = new Frame();
			frame.DoRoll(5);
			frame.DoRoll(5);
			game.AddFrame(frame);
			Assert.AreEqual(0, game.GetFrameScore(4));
			Assert.AreEqual(33, game.Score());
			frame = new Frame();
			game.AddFrame(frame);
			Assert.AreEqual(33, game.Score());
			frame.DoRoll(8);
			Assert.AreEqual(59, game.Score());
			frame.DoRoll(1);
			Assert.AreEqual(60, game.Score());
			frame = new Frame();
			frame.DoRoll(2);
			frame.DoRoll(4);
			game.AddFrame(frame);
			frame = new Frame();
			frame.DoRoll(5);
			frame.DoRoll(5);
			game.AddFrame(frame);
			frame = new Frame();
			frame.DoRoll(8);
			frame.DoRoll(1);
			game.AddFrame(frame);
			frame = new Frame();
			frame.DoRoll(2);
			frame.DoRoll(4);
			game.AddFrame(frame);
			frame = new Frame();
			frame.DoRoll(2);
			frame.DoRoll(4);
			game.AddFrame(frame);
			Assert.AreEqual(true, game.IsFinished);
			Assert.AreEqual(105, game.Score());
		}
	}
}