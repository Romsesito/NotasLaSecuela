using CommunityToolkit.Mvvm.Input;
using Notes.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Notes.ViewModels;

internal class JANotesViewModel : IQueryAttributable
{
    public ObservableCollection<ViewModels.JANoteViewModel> AllNotes { get; }
    public ICommand NewCommand { get; }
    public ICommand SelectNoteCommand { get; }

    public JANotesViewModel()
    {
        AllNotes = new ObservableCollection<ViewModels.JANoteViewModel>(Models.JANote.LoadAll().Select(n => new JANoteViewModel(n)));
        NewCommand = new AsyncRelayCommand(NewNoteAsync);
        SelectNoteCommand = new AsyncRelayCommand<ViewModels.JANoteViewModel>(SelectNoteAsync);
    }

    private async Task NewNoteAsync()
    {
        await Shell.Current.GoToAsync(nameof(Views.NotePage));
    }

    private async Task SelectNoteAsync(ViewModels.JANoteViewModel note)
    {
        if (note != null)
            await Shell.Current.GoToAsync($"{nameof(Views.NotePage)}?load={note.Identifier}");
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted"))
        {
            string noteId = query["deleted"].ToString();
            JANoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

            // If note exists, delete it
            if (matchedNote != null)
                AllNotes.Remove(matchedNote);
        }
        else if (query.ContainsKey("saved"))
        {
            string noteId = query["saved"].ToString();
            JANoteViewModel matchedNote = AllNotes.Where((n) => n.Identifier == noteId).FirstOrDefault();

            // If note is found, update it
            if (matchedNote != null)
            {
                matchedNote.Reload();
                AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
            }
            // If note isn't found, it's new; add it.
            else
                AllNotes.Insert(0, new JANoteViewModel(Models.JANote.Load(noteId)));
        }
    }
}