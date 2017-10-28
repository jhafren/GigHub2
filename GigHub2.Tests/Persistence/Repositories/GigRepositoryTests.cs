using FluentAssertions;
using GigHub2.Core.Models;
using GigHub2.Persistence;
using GigHub2.Persistence.Repositories;
using GigHub2.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;

namespace GigHub2.Tests.Persistence.Repositories
{

    [TestClass]
    public class GigRepositoryTests
    {
        private GigRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGigs = new Mock<DbSet<Gig>>();
            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(m => m.Gigs).Returns(_mockGigs.Object);

            _repository = new GigRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsInThePast_ShouldNotBeReturned()
        {
            var artistId = "1";
            var gig = new Gig { DateTime = DateTime.Now.AddDays(-2), ArtistId = artistId };
            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(artistId);

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsCanceled_ShouldNotBeReturned()
        {
            var artistId = "1";
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = artistId };
            gig.Cancel();
            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(artistId);

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForAnotherArtist_ShouldNotBeReturned()
        {
            var artistId = "1";
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = artistId };
            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(artistId + "-");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForTheGivenUserAndIsInTheFuture_ShouldBeReturned()
        {
            var artistId = "1";
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = artistId };
            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(artistId);

            gigs.Should().Contain(gig);
        }
    }
}
