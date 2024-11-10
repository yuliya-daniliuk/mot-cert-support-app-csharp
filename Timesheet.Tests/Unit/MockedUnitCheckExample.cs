namespace Timesheet.Tests.Unit;

using Moq;
using Timesheet.DB;
using Timesheet.Models.Project;

public class MockedUnitCheckExample
{

   private readonly Mock<IProjectDB> _projectDBMock = new Mock<IProjectDB>();

   [Test]
   public void ReturnPositiveWhenEntryCreated()
   {
      Entry entry = new Entry(DateTime.Parse("2023-01-01"), 8, "Ate cake");
       
      _projectDBMock.Setup(_projectDBMock => _projectDBMock.StoreEntry(1, entry)).Returns(1);
   }

}
