using System;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;

namespace Ploker.Domain.Test
{
    public class TableTest
    {
        [Fact]
        public void GivenANewTable_WhenGetStatus_ThenAnEmptyStatusIsReturned()
        {
            var table = new Table();
            var status = table.GetStatus();
            status.Should().BeEmpty();
        }

        [Fact]
        public void GivenAnEmptyTable_WhenAddPlayer_ThenStatusShowsOnePlayerEmptyHand()
        {
            var table = new Table();
            table.AddPlayer("Phil");
            var status = table.GetStatus();
            status.Should().HaveCount(1);
            status.ShouldBeEquivalentTo(new [] { new PlayerStatus("Phil", "")});
        }

        [Fact]
        public void GivenAnEmptyTable_WhenAddingTheSamePlayerTwice_ThenGetStatusShowsOnePlayer()
        {
            var table = new Table();
            table.AddPlayer("Phil");
            table.AddPlayer("Phil");
            var status = table.GetStatus();
            status.Should().HaveCount(1);
            status.ShouldBeEquivalentTo(new [] { new PlayerStatus("Phil", "")});
        }

        [Fact]
        public void GivenATableWithSomePlayers_WhenSetHandForOnePlayer_ThenGetStatusShowsAConcealedHandForThatPlayer()
        {
            var table = new Table();
            table.AddPlayer("Phil");
            table.AddPlayer("Daniel");
            table.SetHandFor("Phil", 3);
            var status = table.GetStatus();
            status.Should().HaveCount(2);
            status.Should().Contain(p => p.Name == "Phil" && p.Hand == "X");
            status.Should().Contain(p => p.Name == "Daniel" && p.Hand == "");
        }

        [Fact]
        public void GivenATableWithSomePlayers_WhenAllPlayersHaveAHand_ThenGetStatusShowsTheHands()
        {
            var table = new Table();
            table.AddPlayer("Phil");
            table.AddPlayer("Daniel");
            table.SetHandFor("Phil", 3);
            table.SetHandFor("Daniel", 5);
            var status = table.GetStatus();
            status.Should().HaveCount(2);
            status.Should().Contain(p => p.Name == "Phil" && p.Hand == "3");
            status.Should().Contain(p => p.Name == "Daniel" && p.Hand == "5");
        }

        [Fact]
        public void GivenATableWithAPlayer_WhenRemoveThatPlayer_ThenStatusShowsNoPlayers()
        {
            var table = new Table();
            table.AddPlayer("Phil");
            table.GetStatus().Should().HaveCount(1);
            table.RemovePlayer("Phil");
            var status = table.GetStatus();
            table.GetStatus().Should().BeEmpty();
        }
    }
}
