using SimpleNotes.Application.Models;
using SimpleNotes.Infrastructure.Enums;

namespace SimpleNotes.Infrastructure.Entities;

public class Note : TreeNode
{
    public string? Text { get; set; }
    
    public static Note CreateFromDto(Guid id, CreateNoteDto createNoteDto)
    {
        return new Note
        {
            Id = id,
            Type = NodeType.Note,
            Name = createNoteDto.Name,
            Text = createNoteDto.Text,
            TreeNodeLabels = createNoteDto.LabelIds
                ?.Select(l => new TreeNodeLabel
                {
                    LabelId = l
                })
                .ToHashSet()
        };
    }
}