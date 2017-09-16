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
            status.Should().Contain(new KeyValuePair<string, string>("Phil", ""));
        }

        [Fact]
        public void GivenAnEmptyTable_WhenAddingTheSamePlayerTwice_ThenGetStatusShowsOnePlayer()
        {
            var table = new Table();
            table.AddPlayer("Phil");
            table.AddPlayer("Phil");
            var status = table.GetStatus();
            status.Should().HaveCount(1);
            status.ContainsKey("Phil").Should().BeTrue();
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
            status.Should().Contain(new KeyValuePair<string, string>("Phil", "X"));
            status.Should().Contain(new KeyValuePair<string, string>("Daniel", ""));
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
            status.Should().Contain(new KeyValuePair<string, string>("Phil", "3"));
            status.Should().Contain(new KeyValuePair<string, string>("Daniel", "5"));
        }
    }
}
