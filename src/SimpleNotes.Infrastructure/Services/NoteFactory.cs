using Microsoft.EntityFrameworkCore;
using SimpleNotes.Application.Abstractions;
using SimpleNotes.Application.Models;
using SimpleNotes.Infrastructure.Entities;
using SimpleNotes.Infrastructure.Enums;

namespace SimpleNotes.Infrastructure.Services;

public class NoteFactory(IIdGenerator idGenerator) : INoteFactory
{
    public Note Create(CreateNoteDto createNoteDto, string parentPath)
    {
        var id = idGenerator.NewId();
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
                .ToHashSet(),
            Path = new LTree($"{parentPath}.{id}")
        };
    }
}