using System;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

namespace Ploker.Domain.Test
{
    public class TableTest
    {
        [Fact]
        public void GivenANewTable_WhenGetStatus_ThenAnEmptyStatusIsReturned()
        {
            var table = new Table(1);
            table.GetStatus().Players.Should().BeEmpty();
        }

        [Fact]
        public void GivenATable_WhenGetStatus_ThenTheIdisFilledIn()
        {
            var table = new Table(123);
            table.GetStatus().Id.Should().Be(123);
        }

        [Fact]
        public void GivenAnEmptyTable_WhenAddPlayer_ThenStatusShowsOnePlayerEmptyHand()
        {
            var table = new Table(1);
            table.AddPlayer("Phil");
            var status = table.GetStatus();
            status.Players.Should().HaveCount(1);
            status.Players.ShouldBeEquivalentTo(new [] { new PlayerStatus("Phil", "")});
        }

        [Fact]
        public void GivenAnEmptyTable_WhenAddingTheSamePlayerTwice_ThenGetStatusShowsOnePlayer()
        {
            var table = new Table(1);
            table.AddPlayer("Phil");
            table.AddPlayer("Phil");
            var status = table.GetStatus();
            status.Players.Should().HaveCount(1);
            status.Players.ShouldBeEquivalentTo(new [] { new PlayerStatus("Phil", "")});
        }

        [Fact]
        public void GivenATableWithSomePlayers_WhenSetHandForOnePlayer_ThenGetStatusShowsAConcealedHandForThatPlayer()
        {
            var table = new Table(1);
            table.AddPlayer("Phil");
            table.AddPlayer("Daniel");
            table.SetHandFor("Phil", 3);
            var status = table.GetStatus();
            status.Players.Should().HaveCount(2);
            status.Players.Should().Contain(p => p.Name == "Phil" && p.Hand == "X");
            status.Players.Should().Contain(p => p.Name == "Daniel" && p.Hand == "");
        }

        [Fact]
        public void GivenATableWithSomePlayers_WhenAllPlayersHaveAHand_ThenGetStatusShowsTheHands()
        {
            var table = new Table(1);
            table.AddPlayer("Phil");
            table.AddPlayer("Daniel");
            table.SetHandFor("Phil", 3);
            table.SetHandFor("Daniel", 5);
            var status = table.GetStatus();
            status.Players.Should().HaveCount(2);
            status.Players.Should().Contain(p => p.Name == "Phil" && p.Hand == "3");
            status.Players.Should().Contain(p => p.Name == "Daniel" && p.Hand == "5");
        }

        [Fact]
        public void GivenATableWithAPlayer_WhenRemoveThatPlayer_ThenStatusShowsNoPlayers()
        {
            var table = new Table(1);
            table.AddPlayer("Phil");
            table.GetStatus().Players.Should().HaveCount(1);
            table.RemovePlayer("Phil");
            table.GetStatus().Players.Should().BeEmpty();
        }

        [Fact]
        public void GivenATableWithAPlayer_WhenDealOutThatPlayer_ThenStatusShowsNoPlayers()
        {
            var table = new Table(1);
            table.AddPlayer("Phil");
            table.GetStatus().Players.Should().HaveCount(1);
            table.DealOut("Phil");
            table.GetStatus().Players.Should().BeEmpty();
        }

        [Fact]
        public void GivenATableWithAPlayerThatIsDealtOut_WhenDealInThatPlayer_ThenStatusShowsOnePlayer()
        {
            var table = new Table(1);
            table.AddPlayer("Phil");
            table.DealOut("Phil");
            table.GetStatus().Players.Should().BeEmpty();
            table.DealIn("Phil");
            table.GetStatus().Players.Should().HaveCount(1);   
        }

        [Fact]
        public void GivenATableWithSomePlayersWithSomeHands_WhenReset_ThenStatusShowsNoHands()
        {
            var table = new Table(1);
            table.AddPlayer("Phil");
            table.AddPlayer("Daniel");
            table.SetHandFor("Phil", 3);
            table.SetHandFor("Daniel", 5);
            table.GetStatus().Players.Where(p => !string.IsNullOrWhiteSpace(p.Hand)).Should().HaveCount(2);
            table.Reset();
            table.GetStatus().Players.Where(p => !string.IsNullOrWhiteSpace(p.Hand)).Should().BeEmpty();
        }

        [Fact]
        public void GivenAnEmptyTable_WhenIsEmpty_ThenReturnsTrue()
        {
            var table = new Table(1);
            table.IsEmpty().Should().BeTrue();
        }

        [Fact]
        public void GivenATableWithSomePlayers_WhenIsEmpty_ThenReturnsFalse()
        {
            var table = new Table(1);
            table.AddPlayer("Phil");
            table.AddPlayer("Daniel");
            table.IsEmpty().Should().BeFalse();
        }
    }
}
