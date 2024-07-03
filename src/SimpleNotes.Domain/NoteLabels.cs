using SimpleNotes.Domain.Common;

namespace SimpleNotes.Domain;

public class NoteLabels : Entity<Guid>
{
    private readonly Dictionary<Guid, LabelState> _labelIds;

    public NoteLabels(Guid id, IEnumerable<Guid> labelIds) : base(id)
    {
        _labelIds = labelIds.ToDictionary(labelId => labelId, _ => LabelState.Default);
    }

    public bool AssignLabel(Guid labelId)
    {
        return _labelIds.TryAdd(labelId, LabelState.Assigned);
    }

    public IEnumerable<Guid> NewLabelsIterator()
    {
        return _labelIds.Where(kvp => kvp.Value is LabelState.Assigned).Select(kvp => kvp.Key);
    }
}