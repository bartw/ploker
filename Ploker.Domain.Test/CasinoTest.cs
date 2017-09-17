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
        public void GivenACasinoWithATable_WhenRemoveTable_ThenThatTableIsRemoved()
        {
            var casino = new Casino();
            var id = casino.CreateTable();
            casino.RemoveTable(id);
            var table = casino.GetTable(id);
            table.Should().BeNull();
        }

        [Fact]
        public void GivenACasino_WhenRemoveTableThatDoesNotExist_ThenNothingHappens()
        {
            var casino = new Casino();
            var id = casino.CreateTable();
            casino.RemoveTable(id + 1);
            var table = casino.GetTable(id);
            table.Should().NotBeNull();
        }
    }
}