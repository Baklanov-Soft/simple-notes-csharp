namespace SimpleNotes.Domain.UnitTests;

public class NoteLabelsTests
{
    private readonly NoteLabels _note = new(Guid.Empty, []);

    [Fact]
    public void Assign_labels()
    {
        var label1 = Guid.ParseExact("019079de-4295-7e83-821c-868f3b2e7578", "D");
        var label2 = Guid.ParseExact("019079de-cfad-7401-9843-9aa099e06b07", "D");
        
        var result1 = _note.AssignLabel(label1);
        var result2 = _note.AssignLabel(label1);
        var result3 = _note.AssignLabel(label2);

        result1.Should().BeTrue();
        result2.Should().BeFalse();
        result3.Should().BeTrue();
        _note.NewLabelsIterator().SequenceEqual([label1, label2]).Should().BeTrue();
    }
}