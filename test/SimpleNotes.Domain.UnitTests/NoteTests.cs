namespace SimpleNotes.Domain.UnitTests;

public class NoteTests
{
    public class ChangingName
    {
        private readonly Note _note = new(Guid.Empty, "Test", 10);

        [Fact]
        public void Change_name_to_empty()
        {
            var result = _note.ChangeName(string.Empty);
         
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().HaveCount(1);
            result.Errors.First().Should().BeOfType<NoteNameIsNullOrWhiteSpaceError>();
        }

        [Fact]
        public void Change_name_to_too_long()
        {
            var result = _note.ChangeName("12345678901");
         
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().HaveCount(1);
            result.Errors.First().Should().BeOfType<NoteNameLengthLimitExceededError>();
        }
        
        [Fact]
        public void Change_name_to_normal_name()
        {
            var result = _note.ChangeName("Test");
         
            result.IsSuccess.Should().BeTrue();
            
            _note.Name.Should().Be("Test");
        }
    }
    
    public class AssignLabels
    {
        private readonly Note _note = new(Guid.Empty, "TestNote", 10);
        private readonly Guid _labelId = Guid.Empty;

        [Fact]
        public void Assign_single_label()
        {
            var result = _note.AssignLabel(_labelId);
         
            result.IsSuccess.Should().BeTrue();
            
            _note.LabelIds.Should().Contain(_labelId);
        }

        [Fact]
        public void Assign_label_twice()
        {
            _note.AssignLabel(_labelId);
            var result = _note.AssignLabel(_labelId);
         
            result.IsFailed.Should().BeTrue();
            result.Errors.Should().HaveCount(1);
            result.Errors.First().Should().BeOfType<LabelAlreadyAssignedError>();
            
        }
    }
}