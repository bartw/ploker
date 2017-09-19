using System;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

namespace Ploker.Domain.Test
{
    public class CasinoTest
    {
        [Fact]
        public void GivenACasino_WhenCreatTable_ThenANewUniqueTableIdIsReturned()
        {
            var ids = new List<int>();
            var casino = new Casino();
            for (int i = 0; i < 10; i++)
            {
                ids.Add(casino.CreateTable());
            }
            Assert.True(ids.Distinct().Count() == ids.Count());
        }

        [Fact]
        public void GivenACasinoWith10Tables_WhenCreateTable_AnExceptionIsThrown()
        {
            var casino = new Casino();
            for (int i = 0; i < 10; i++)
            {
                casino.CreateTable();
            }

            Action action = () => casino.CreateTable();
            action.ShouldThrow<Exception>().WithMessage("Reached the maximum of 10 tables.");
        }

        [Fact]
        public void GivenACasinoWithATable_WhenGetTable_ThenTheTableIsReturned()
        {
            var casino = new Casino();
            var id = casino.CreateTable();
            var table = casino.GetTable(id);
            table.Should().NotBeNull();
        }

        [Fact]
        public void GivenACasino_WhenGetTableThatDoesNotExist_ThenNullIsReturned()
        {
            var casino = new Casino();
            var table = casino.GetTable(123);
            table.Should().BeNull();
        }

        [Fact]
        public void GivenACasinoWithATableWithOnePlayer_WhenRemovePlayer_ThenThatTableIsRemoved()
        {
            var casino = new Casino();
            var id = casino.CreateTable();
            var table = casino.GetTable(id);
            table.AddPlayer("Phil");
            casino.RemovePlayer("Phil");
            casino.GetTable(id).Should().BeNull();
        }

        [Fact]
        public void GivenACasino_WhenRemovePlayerThatDoesNotExist_ThenNothingHappens()
        {
            var casino = new Casino();
            var id = casino.CreateTable();
            casino.GetTable(id).AddPlayer("Daniel");
            casino.RemovePlayer("Phil");
            var table = casino.GetTable(id);
            table.Should().NotBeNull();
        }

        [Fact]
        public void GivenACasinoWithATableWithMultiplePlayers_WhenRemovePlayer_ThenThePlayerIsRemovedButNotTheTable()
        {
            var casino = new Casino();
            var id = casino.CreateTable();
            casino.GetTable(id).AddPlayer("Phil");
            casino.GetTable(id).AddPlayer("Daniel");
            casino.RemovePlayer("Phil");
            var table = casino.GetTable(id);
            table.Should().NotBeNull();
            var status = table.GetStatus();
            status.Players.Should().HaveCount(1);
            status.Players.Should().Contain(p => p.Name == "Daniel");
        }

        [Fact]
        public void GivenACasinoWithSomeTablesWithPlayers_WhenGetTablesForPlayer_ThenTheCorrectTablesAreReturned()
        {
            var casino = new Casino();
            var id1 = casino.CreateTable();
            var id2 = casino.CreateTable();
            var id3 = casino.CreateTable();
            casino.GetTable(id1).AddPlayer("Phil");
            casino.GetTable(id2).AddPlayer("Daniel");
            casino.GetTable(id3).AddPlayer("Phil");
            casino.GetTablesFor("Phil").Should().BeEquivalentTo(new [] {id1, id3});
        }
    }
}