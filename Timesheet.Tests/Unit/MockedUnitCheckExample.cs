namespace Timesheet.Tests.Unit;

using Moq;
using Timesheet.DB;
using Timesheet.Models;
using Timesheet.Models.Project;
using Timesheet.Service;

public class MockedUnitCheckExample
{
   private readonly Mock<IProjectDB> _projectDBMock = new Mock<IProjectDB>();

   [Test]
   public void ReturnPositiveWhenEntryCreated()
   {
      Entry entry = new Entry(DateTime.Parse("2023-01-01"), 8, "Ate cake");
       
      _projectDBMock.Setup(_projectDBMock => _projectDBMock.StoreEntry(1, entry)).Returns(1);
      ProjectService _projectService = new ProjectService(_projectDBMock.Object);

       (int code, CreatedID createdID) = _projectService.CreateEntry(1, entry);
       _projectDBMock.Verify(_projectDBMock => _projectDBMock.StoreEntry(1, entry), Times.Once);
   }

}
