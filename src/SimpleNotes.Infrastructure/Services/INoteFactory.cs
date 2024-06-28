using SimpleNotes.Application.Models;
using SimpleNotes.Infrastructure.Entities;

namespace SimpleNotes.Infrastructure.Services;

public interface INoteFactory
{
    Note Create(CreateNoteDto createNoteDto, string parentPath);
} 