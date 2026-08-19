using Godot;
using System;

public partial class NoteUi : Control
{
	[Export] Label _note;

    [Export] TextureButton _exitButton;
	public override void _Ready()
	{
		Hide();
		SignalHub.Instance.OnNoteInteracted += OnNoteInteracted;
        _exitButton.Pressed += ExitButtonPressed;
	}

    private void ExitButtonPressed()
    {
        AudioManager.Instance.PlayUIClickSound();
		Hide();
		GetTree().Paused = false;
    }


    public override void _ExitTree()
    {
        SignalHub.Instance.OnNoteInteracted -= OnNoteInteracted;
    }


    private void OnNoteInteracted(string noteCode)
    {
		if (NoteDatabaseManager.Instance.Notes.TryGetValue(noteCode, out NoteEntry specificNote))
		{

            _note.Text = specificNote.Body.Trim().Replace("\n", " ").Replace("  ", " ");
			FreezeAndShow();
			
		}
        else
        {
            GD.PrintErr($"Could not find a note with ID: {noteCode} in the JSON file.");
        }
    }

    private void FreezeAndShow()
    {
        GetTree().Paused = true;
		Show();
    }

}
