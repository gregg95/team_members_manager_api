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
            // Arrange
            string name = "John Doe";
            string email = "john@example.com";
            string phoneNumber = "1234567890";
            DateTime createdAt = DateTime.UtcNow;

            // Act
            var teamMember = TeamMember.Create(name, email, phoneNumber, createdAt);

            // Assert
            Assert.That(teamMember.Name, Is.EqualTo(name));
            Assert.That(teamMember.Email, Is.EqualTo(email));
            Assert.That(teamMember.PhoneNumber, Is.EqualTo(phoneNumber));
            Assert.That(teamMember.Status, Is.EqualTo(TeamMemberStatus.Active));
            Assert.That(teamMember.CreatedAtDateTime, Is.EqualTo(createdAt));
        }

        [Test]
        public void When_ActivateInActiveMember_Then_StatusShouldBeActive()
        {
            // Arrange
            var teamMember = TeamMember.Create("John Doe", "john@example.com", "1234567890", DateTime.UtcNow);
            teamMember.Block();

            // Act
            teamMember.Activate();

            // Assert
            Assert.That(teamMember.Status, Is.EqualTo(TeamMemberStatus.Active));
        }

        [Test]
        public void When_ActivateActiveMember_Then_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var teamMember = TeamMember.Create("John Doe", "john@example.com", "1234567890", DateTime.UtcNow);

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => teamMember.Activate());
            Assert.That(ex.Message, Is.EqualTo("Team member is already active"));
        }

        [Test]
        public void When_BlockActiveMember_Then_StatusShouldBeBlocked()
        {
            // Arrange
            var teamMember = TeamMember.Create("John Doe", "john@example.com", "1234567890", DateTime.UtcNow);

            // Act
            teamMember.Block();

            // Assert
            Assert.That(teamMember.Status, Is.EqualTo(TeamMemberStatus.Blocked));
        }

        [Test]
        public void When_BlockBlockedMember_Then_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var teamMember = TeamMember.Create("John Doe", "john@example.com", "1234567890", DateTime.UtcNow);
            teamMember.Block();

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => teamMember.Block());
            Assert.That(ex.Message, Is.EqualTo("Team member is already blocked"));
        }

        [Test]
        public void When_UpdateEmail_Then_EmailShouldBeUpdated()
        {
            // Arrange
            var teamMember = TeamMember.Create("John Doe", "john@example.com", "1234567890", DateTime.UtcNow);
            string newEmail = "newjohn@example.com";

            // Act
            teamMember.UpdateEmail(newEmail);

            // Assert
            Assert.That(teamMember.Email, Is.EqualTo(newEmail));
        }

        [Test]
        public void When_UpdatingName_Then_ShouldUpdateName()
        {
            // Arrange
            var teamMember = TeamMember.Create("Old Name", "user@example.com", "4321432143", DateTime.UtcNow);
            var newName = "New Name";

            // Act
            teamMember.UpdateName(newName);

            // Assert
            Assert.That(teamMember.Name, Is.EqualTo(newName));
        }

        [Test]
        public void When_UpdatingPhoneNumber_Then_ShouldUpdatePhoneNumber()
        {
            // Arrange
            var teamMember = TeamMember.Create("User", "user@example.com", "oldphone", DateTime.UtcNow);
            var newPhone = "newphone";

            // Act
            teamMember.UpdatePhoneNumber(newPhone);

            // Assert
            Assert.That(teamMember.PhoneNumber, Is.EqualTo(newPhone));
        }
    }
}
