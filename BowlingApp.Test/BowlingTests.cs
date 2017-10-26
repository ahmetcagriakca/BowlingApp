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
			Frame frame = game.CreateFrame();
			frame.DoRoll(5);
			frame.DoRoll(4);
			Assert.AreEqual(9, game.GetFrameScore(1));
		}

		/// <summary>
		/// Scenario 2
		/// </summary>
		[TestMethod]
		public void SixRoll()
		{
			Game game = new Game();
			Frame frame = game.CreateFrame();
			frame.DoRoll(5);
			frame.DoRoll(4);
			frame = game.CreateFrame();
			frame.DoRoll(8);
			frame.DoRoll(1);
			frame = game.CreateFrame();
			frame.DoRoll(2);
			frame.DoRoll(4);
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
				Frame frame = game.CreateFrame();
				for (int j = 0; j < 2; j++)
				{
					frame.DoRoll(0);
				}
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
				Frame frame = game.CreateFrame();
				for (int j = 0; j < 2; j++)
				{
					frame.DoRoll(1);
				}
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
				Frame frame = game.CreateFrame();
				int pinCount = frame.GetRemainingPinCount();
				int rollPins = random.Next(pinCount);
				totalScore += rollPins;
				frame.DoRoll(rollPins);
				pinCount = frame.GetRemainingPinCount();
				if (pinCount > 0)
				{
					rollPins = random.Next(pinCount);
					totalScore += rollPins;
					frame.DoRoll(rollPins);
				}
			}
			Assert.AreEqual(totalScore, game.Score(), totalScore);
		}

		/// <summary>
		/// Scenario 6
		/// </summary>
		[TestMethod]
		public void SixRollWithOneSpare()
		{
			Game game = new Game();
			Frame frame = game.CreateFrame();
			frame.DoRoll(5);
			frame.DoRoll(5);
			frame = game.CreateFrame();
			frame.DoRoll(8);
			frame.DoRoll(1);
			frame = game.CreateFrame();
			frame.DoRoll(2);
			frame.DoRoll(4);
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
			Frame frame = game.CreateFrame();
			frame.DoRoll(5);
			frame.DoRoll(5);
			frame = game.CreateFrame();
			frame.DoRoll(8);
			frame.DoRoll(1);
			frame = game.CreateFrame();
			frame.DoRoll(2);
			frame.DoRoll(4);
			frame = game.CreateFrame();
			frame.DoRoll(5);
			frame.DoRoll(5);
			Assert.AreEqual(0, game.GetFrameScore(4));
			Assert.AreEqual(33, game.Score());
			frame = game.CreateFrame();
			Assert.AreEqual(33, game.Score());
			frame.DoRoll(8);
			Assert.AreEqual(51, game.Score());
			frame.DoRoll(1);
			Assert.AreEqual(60, game.Score());
			frame = game.CreateFrame();
			frame.DoRoll(2);
			frame.DoRoll(4);
			frame = game.CreateFrame();
			frame.DoRoll(5);
			frame.DoRoll(5);
			frame = game.CreateFrame();
			frame.DoRoll(8);
			frame.DoRoll(1);
			frame = game.CreateFrame();
			frame.DoRoll(2);
			frame.DoRoll(4);
			frame = game.CreateFrame();
			frame.DoRoll(2);
			frame.DoRoll(4);
			Assert.AreEqual(true, game.IsFinished);
			Assert.AreEqual(105, game.Score());
		}

		/// <summary>
		/// Scenario 8
		/// </summary>
		[TestMethod]
		public void FiveRollWithOneStrike()
		{
			Game game = new Game();
			Frame frame = game.CreateFrame();
			frame.DoRoll(10);
			frame = game.CreateFrame();
			frame.DoRoll(8);
			frame.DoRoll(1);
			frame = game.CreateFrame();
			frame.DoRoll(2);
			frame.DoRoll(4);
			Assert.AreEqual(19, game.GetFrameScore(1));
			Assert.AreEqual(34, game.Score());
		}

		/// <summary>
		/// Scenario 9
		/// </summary>
		[TestMethod]
		public void FourRollWithTwoStrike()
		{
			Game game = new Game();
			Frame frame = game.CreateFrame();
			frame.DoRoll(10);
			frame = game.CreateFrame();
			frame.DoRoll(10);
			frame = game.CreateFrame();
			frame.DoRoll(1);
			frame.DoRoll(2);
			Assert.AreEqual(21, game.GetFrameScore(1));
			Assert.AreEqual(13, game.GetFrameScore(2));
			Assert.AreEqual(37, game.Score());
		}

		/// <summary>
		/// Scenario 10
		/// </summary>
		[TestMethod]
		public void FifteenRollWithFiveStrike()
		{
			Game game = new Game();
			Frame frame = game.CreateFrame();
			frame.DoRoll(10);
			frame = game.CreateFrame();
			frame.DoRoll(10);
			frame = game.CreateFrame();
			frame.DoRoll(10);
			Assert.AreEqual(0, game.GetFrameScore(2));
			Assert.AreEqual(30, game.Score());
			frame = game.CreateFrame();
			frame.DoRoll(8);
			Assert.AreEqual(28, game.GetFrameScore(2));
			Assert.AreEqual(58, game.Score());
			frame.DoRoll(1);
			Assert.AreEqual(86, game.Score());
			frame = game.CreateFrame();
			frame.DoRoll(2);
			frame.DoRoll(4);
			frame = game.CreateFrame();
			frame.DoRoll(10);
			frame = game.CreateFrame();
			frame.DoRoll(8);
			frame.DoRoll(1);
			frame = game.CreateFrame();
			frame.DoRoll(2);
			frame.DoRoll(4);
			frame = game.CreateFrame();
			frame.DoRoll(10);
			frame = game.CreateFrame();
			frame.DoRoll(2);
			frame.DoRoll(4);
			Assert.AreEqual(true, game.IsFinished);
			Assert.AreEqual(30, game.GetFrameScore(1));
			Assert.AreEqual(28, game.GetFrameScore(2));
			Assert.AreEqual(19, game.GetFrameScore(3));
			Assert.AreEqual(16, game.GetFrameScore(9));
			Assert.AreEqual(148, game.Score());
		}

		/// <summary>
		/// Scenario 11
		/// </summary>
		[TestMethod]
		public void SixteenRollWithFiveStrikeAndOneSpareOnLastGame()
		{
			Game game = new Game();
			Frame frame = game.CreateFrame();//30
			frame.DoRoll(10);
			frame = game.CreateFrame();//28
			frame.DoRoll(10);
			frame = game.CreateFrame();//19
			frame.DoRoll(10);
			Assert.AreEqual(0, game.GetFrameScore(2));
			Assert.AreEqual(30, game.Score());
			frame = game.CreateFrame();//9
			frame.DoRoll(8);
			Assert.AreEqual(28, game.GetFrameScore(2));
			Assert.AreEqual(58, game.Score());
			frame.DoRoll(1);
			Assert.AreEqual(86, game.Score());
			frame = game.CreateFrame();//6
			frame.DoRoll(2);
			frame.DoRoll(4);
			frame = game.CreateFrame();//19
			frame.DoRoll(10);
			frame = game.CreateFrame();//9
			frame.DoRoll(8);
			frame.DoRoll(1);
			frame = game.CreateFrame();//6
			frame.DoRoll(2);
			frame.DoRoll(4);
			frame = game.CreateFrame();//20
			frame.DoRoll(10);
			frame = game.CreateFrame();//26
			frame.DoRoll(2);
			frame.DoRoll(8);
			Assert.AreEqual(false, game.IsFinished);
			frame.DoRoll(8);
			Assert.AreEqual(true, game.IsFinished);
			Assert.AreEqual(30, game.GetFrameScore(1));
			Assert.AreEqual(28, game.GetFrameScore(2));
			Assert.AreEqual(19, game.GetFrameScore(3));
			Assert.AreEqual(20, game.GetFrameScore(9));
			Assert.AreEqual(18, game.GetFrameScore(10));
			Assert.AreEqual(164, game.Score());
		}


		/// <summary>
		/// Scenario 12
		/// </summary>
		[TestMethod]
		public void SixteenRollWithFiveStrikeAndOneStrikeOnLastGame()
		{
			Game game = new Game();
			Frame frame = game.CreateFrame();//30
			frame.DoRoll(10);
			frame = game.CreateFrame();//28
			frame.DoRoll(10);
			frame = game.CreateFrame();//19
			frame.DoRoll(10);
			Assert.AreEqual(0, game.GetFrameScore(2));
			Assert.AreEqual(30, game.Score());
			frame = game.CreateFrame();//9
			frame.DoRoll(8);
			Assert.AreEqual(28, game.GetFrameScore(2));
			Assert.AreEqual(58, game.Score());
			frame.DoRoll(1);
			Assert.AreEqual(86, game.Score());
			frame = game.CreateFrame();//6
			frame.DoRoll(2);
			frame.DoRoll(4);
			frame = game.CreateFrame();//19
			frame.DoRoll(10);
			frame = game.CreateFrame();//9
			frame.DoRoll(8);
			frame.DoRoll(1);
			frame = game.CreateFrame();//6
			frame.DoRoll(2);
			frame.DoRoll(4);
			frame = game.CreateFrame();//20
			frame.DoRoll(10);
			frame = game.CreateFrame();//26
			frame.DoRoll(10);
			Assert.AreEqual(0, game.GetFrameScore(9));
			frame.DoRoll(8);
			Assert.AreEqual(28, game.GetFrameScore(9));
			Assert.AreEqual(false, game.IsFinished);
			frame.DoRoll(9);
			Assert.AreEqual(true, game.IsFinished);
			Assert.AreEqual(30, game.GetFrameScore(1));
			Assert.AreEqual(28, game.GetFrameScore(2));
			Assert.AreEqual(19, game.GetFrameScore(3));
			Assert.AreEqual(28, game.GetFrameScore(9));
			Assert.AreEqual(27, game.GetFrameScore(10));
			Assert.AreEqual(181, game.Score());
		}



		/// <summary>
		/// Scenario 3
		/// </summary>
		[TestMethod]
		public void AllRollsAreStrike()
		{
			Game game = new Game();
			Frame frame = null;

			void checkFrameScore()
			{
				game.GetFrameScore(3);
			}
			Assert.ThrowsException<Exception>(new Action(checkFrameScore));
			for (int i = 0; i < 9; i++)
			{
				frame = game.CreateFrame();
				for (int j = 0; j < 1; j++)
				{
					frame.DoRoll(10);
				}
			}
			Assert.AreEqual(210, game.Score());
			Assert.AreEqual(false, game.IsFinished);
			frame = game.CreateFrame();
			frame.DoRoll(10);
			Assert.AreEqual(240, game.Score());
			Assert.AreEqual(false, game.IsFinished);
			frame.DoRoll(10);
			Assert.AreEqual(270, game.Score());
			Assert.AreEqual(false, game.IsFinished);
			frame.DoRoll(10);
			Assert.AreEqual(true, game.IsFinished);
			Assert.AreEqual(300, game.Score());

			void addFrame()
			{
				game.CreateFrame();
			}
			Assert.ThrowsException<Exception>(new Action(addFrame));
		}
	}
}
