using Domain.Entities;
using Domain.Enums;
using NUnit.Framework;
using InvalidOperationException = Domain.Errors.InvalidOperationException;

namespace UnitTests
{
    [TestFixture]
    public class TeamMemberTests
    {
        [Test]
        public void When_CreateWithValidData_Then_TeamMemberShouldBeCreated()
        {
            // arrange
            string name = "John Doe";
            string email = "john@example.com";
            string phoneNumber = "1234567890";
            DateTime createdAt = DateTime.UtcNow;

            // act
            var teamMember = TeamMember.Create(name, email, phoneNumber, createdAt);

            // assert
            Assert.That(teamMember.Name, Is.EqualTo(name));
            Assert.That(teamMember.Email, Is.EqualTo(email));
            Assert.That(teamMember.PhoneNumber, Is.EqualTo(phoneNumber));
            Assert.That(teamMember.Status, Is.EqualTo(TeamMemberStatus.Active));
            Assert.That(teamMember.CreatedAtDateTime, Is.EqualTo(createdAt));
        }

        [Test]
        public void When_ActivateInactiveMember_Then_StatusShouldBeActive()
        {
            // arrange
            var teamMember = TeamMember.Create("John Doe", "john@example.com", "1234567890", DateTime.UtcNow);
            teamMember.Block();

            // act
            teamMember.Activate();

            // assert
            Assert.That(teamMember.Status, Is.EqualTo(TeamMemberStatus.Active));
        }

        [Test]
        public void When_ActivateActiveMember_Then_ShouldThrowInvalidOperationException()
        {
            // arrange
            var teamMember = TeamMember.Create("John Doe", "john@example.com", "1234567890", DateTime.UtcNow);

            // act & assert
            var ex = Assert.Throws<InvalidOperationException>(() => teamMember.Activate());
            Assert.That(ex.Message, Is.EqualTo("Team member is already active"));
        }

        [Test]
        public void When_BlockActiveMember_Then_StatusShouldBeBlocked()
        {
            // arrange
            var teamMember = TeamMember.Create("John Doe", "john@example.com", "1234567890", DateTime.UtcNow);

            // act
            teamMember.Block();

            // assert
            Assert.That(teamMember.Status, Is.EqualTo(TeamMemberStatus.Blocked));
        }

        [Test]
        public void When_BlockBlockedMember_Then_ShouldThrowInvalidOperationException()
        {
            // arrange
            var teamMember = TeamMember.Create("John Doe", "john@example.com", "1234567890", DateTime.UtcNow);
            teamMember.Block();

            // act & assert
            var ex = Assert.Throws<InvalidOperationException>(() => teamMember.Block());
            Assert.That(ex.Message, Is.EqualTo("Team member is already blocked"));
        }

        [Test]
        public void When_UpdateEmail_Then_EmailShouldBeUpdated()
        {
            // arrange
            var teamMember = TeamMember.Create("John Doe", "john@example.com", "1234567890", DateTime.UtcNow);
            string newEmail = "newjohn@example.com";

            // act
            teamMember.UpdateEmail(newEmail);

            // assert
            Assert.That(teamMember.Email, Is.EqualTo(newEmail));
        }
    }
}
