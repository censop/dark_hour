using Godot;
using System;

public partial class NoteUi : Control
{
	[Export] Label _note;
	public override void _Ready()
	{
		Hide();
		SignalHub.Instance.OnNoteInteracted += OnNoteInteracted;
	}

    public override void _ExitTree()
    {
        SignalHub.Instance.OnNoteInteracted -= OnNoteInteracted;
    }


    private void OnNoteInteracted(string noteCode)
    {
		if (NoteDatabaseManager.Instance.Notes.TryGetValue(noteCode, out NoteEntry specificNote))
		{

            _note.Text = specificNote.Body;
			Show();
			
		}
        else
        {
            GD.PrintErr($"Could not find a note with ID: {noteCode} in the JSON file.");
        }
    }

}
