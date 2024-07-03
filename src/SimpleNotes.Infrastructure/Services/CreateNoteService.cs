using SimpleNotes.Application.Abstractions;
using SimpleNotes.Application.Models;
using SimpleNotes.Infrastructure.DbContexts;
using SimpleNotes.Infrastructure.Entities;

namespace SimpleNotes.Infrastructure.Services;

public class CreateNoteService(INotesDbContext dbContext, INoteFactory noteFactory, INoteRepository noteRepository)
    : ICreateNoteService
{
    public async Task<ReadNoteDto> CreateAsync(CreateNoteDto createNoteDto, Guid? parentId,
        CancellationToken cancellationToken = default)
    {
        Note note;

        if (parentId.HasValue)
        {
            var parentPathResult = await noteRepository.GetFolderPathAsync(parentId.Value, cancellationToken);
            note = noteFactory.Create(createNoteDto, parentPathResult);
        }
        else
        {
            note = noteFactory.Create(createNoteDto);
        }

        dbContext.Notes.Add(note);
        await dbContext.SaveChangesAsync(cancellationToken);

        return note.ToReadDto();
    }
}