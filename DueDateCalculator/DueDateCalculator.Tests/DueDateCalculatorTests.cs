
using DueDateCalculator;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DueDateCalculator.Tests
{
    [TestFixture]
    public class DueDateCalculatorTests
    {
        DueDateCalculator calculator;

        [SetUp]
        public void SetUp() 
        {
            calculator = new DueDateCalculator();
        }

        [Test]
        [TestCase("2022-1-7 15:02:11", 0)]
        public void ZeroHoursShouldDoNothing(DateTime date, float turnaround)
        {            
            DateTime dueDate = calculator.CalculateDueDate(date, turnaround);
            Assert.AreEqual(dueDate, date);
        }

        [Test]
        [TestCase("2022-1-7 15:02:11", 40, "2022-1-14 15:02:11")]
        [TestCase("2021-12-27 15:02:11", 40, "2022-1-3 15:02:11")]
        [TestCase("2022-1-24 15:02:11", 120, "2022-02-14 15:02:11")]
        public void TestsWithAWeekTurnaround(DateTime date, float turnaround, DateTime expectedDueDate)
        {
            DateTime dueDate = calculator.CalculateDueDate(date, turnaround);
            Assert.AreEqual(dueDate, expectedDueDate);
        }

        [Test]
        [TestCase("2022-1-7 15:02:11", 8, "2022-1-10 15:02:11")]
        [TestCase("2022-1-11 15:02:11", 16, "2022-1-13 15:02:11")]
        [TestCase("2022-1-13 15:02:11", 32, "2022-1-19 15:02:11")]
        [TestCase("2022-1-7 15:02:11", 88, "2022-1-24 15:02:11")]
        public void TestWithSeveralDays(DateTime date, float turnaround, DateTime expectedDueDate)
        {
            DateTime dueDate = calculator.CalculateDueDate(date, turnaround);
            Assert.AreEqual(dueDate, expectedDueDate);
        }

        [Test]
        [TestCase("2022-1-7 15:02:11", 1, "2022-1-7 16:02:11")]
        [TestCase("2022-1-7 16:02:11", 1, "2022-1-10 09:02:11")]
        [TestCase("2022-1-7 16:02:11", 10, "2022-1-11 10:02:11")]
        [TestCase("2022-1-10 09:00:11", 5, "2022-1-10 14:00:11")]
        public void TestWithSeveralHours(DateTime date, float turnaround, DateTime expectedDueDate)
        {
            DateTime dueDate = calculator.CalculateDueDate(date, turnaround);
            Assert.AreEqual(dueDate, expectedDueDate);
        }

        [Test]
        [TestCase("2022-1-7 15:02:11", 1.5f, "2022-1-7 16:32:11")]
        [TestCase("2022-1-7 16:02:11", 2.5f, "2022-1-10 10:32:11")]
        [TestCase("2022-1-7 09:02:11", 7.25f, "2022-1-7 16:17:11")]
        [TestCase("2022-1-7 16:02:11", 3.75f, "2022-1-10 11:47:11")]
        [TestCase("2022-1-7 15:02:11", 40.25f, "2022-1-14 15:17:11")]
        [TestCase("2022-1-7 15:02:11", 56.75f, "2022-1-18 15:47:11")]
        public void TestWithFractionHours(DateTime date, float turnaround, DateTime expectedDueDate)
        {
            DateTime dueDate = calculator.CalculateDueDate(date, turnaround);
            Assert.AreEqual(dueDate, expectedDueDate);
        }
    }
}