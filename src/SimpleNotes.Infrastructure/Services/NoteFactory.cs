using FluentResults;
using Microsoft.EntityFrameworkCore;
using SimpleNotes.Application.Abstractions;
using SimpleNotes.Application.Models;
using SimpleNotes.Infrastructure.Entities;

namespace SimpleNotes.Infrastructure.Services;

public class NoteFactory(IIdGenerator idGenerator) : INoteFactory
{
    public Note Create(CreateNoteDto createNoteDto, Result<string> parentPathResult)
    {
        var id = idGenerator.NewId();
        
        var note = Note.CreateFromDto(id, createNoteDto);
        note.Path = parentPathResult switch
        {
            { IsSuccess: true } => new LTree($"{parentPathResult.Value}.{id}"),
            _ => new LTree($"{id}")
        };
        
        return note;
    }

    public Note Create(CreateNoteDto createNoteDto)
    {
        var id = idGenerator.NewId();
        
        var note = Note.CreateFromDto(id, createNoteDto);
        note.Path = new LTree($"{id}");

        return note;
    }
}