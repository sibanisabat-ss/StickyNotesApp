using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace StickyNotesApp
{
    public class NotesListForm : Form
    {
        private ListView notesListView;

        public List<StickyNote> Notes { get; private set; }

        public NotesListForm(List<StickyNote> notes)
        {
            Notes = new List<StickyNote>(notes);
            InitializeComponent();
            LoadNotes();
        }

        private void InitializeComponent()
        {
            this.Text = "Manage Notes";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterParent;

            notesListView = new ListView
            {
                Location = new Point(10, 10),
                Size = new Size(560, 300),
                View = View.Details,
                FullRowSelect = true,
                MultiSelect = false
            };
            notesListView.Columns.Add("Title", 150);
            notesListView.Columns.Add("Created Date", 150);
            notesListView.Columns.Add("Content Preview", 240);
            notesListView.DoubleClick += NotesListView_DoubleClick;

            Button viewButton = new Button
            {
                Text = "View/Edit",
                Location = new Point(10, 320),
                Size = new Size(100, 30)
            };
            viewButton.Click += ViewButton_Click;

            Button deleteButton = new Button
            {
                Text = "Delete",
                Location = new Point(120, 320),
                Size = new Size(100, 30)
            };
            deleteButton.Click += DeleteButton_Click;

            Button closeButton = new Button
            {
                Text = "Close",
                Location = new Point(470, 320),
                Size = new Size(100, 30),
                DialogResult = DialogResult.OK
            };

            this.Controls.Add(notesListView);
            this.Controls.Add(viewButton);
            this.Controls.Add(deleteButton);
            this.Controls.Add(closeButton);
        }

        private void LoadNotes()
        {
            notesListView.Items.Clear();
            foreach (var note in Notes)
            {
                string contentPreview = note.Content;
                if (contentPreview.Length > 30)
                    contentPreview = contentPreview.Substring(0, 27) + "...";

                ListViewItem item = new ListViewItem(note.Title);
                item.SubItems.Add(note.CreatedDate.ToString("MM/dd/yyyy HH:mm"));
                item.SubItems.Add(contentPreview);
                item.Tag = note;
                notesListView.Items.Add(item);
            }
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            if (notesListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a note to view.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ViewOrEditNote();
        }

        private void NotesListView_DoubleClick(object sender, EventArgs e)
        {
            ViewOrEditNote();
        }

        private void ViewOrEditNote()
        {
            StickyNote selectedNote = (StickyNote)notesListView.SelectedItems[0].Tag;
            ViewNoteForm viewForm = new ViewNoteForm(selectedNote);

            if (viewForm.ShowDialog() == DialogResult.OK)
            {
                // Update the note if edited
                selectedNote.Title = viewForm.NoteTitle;
                selectedNote.Content = viewForm.NoteContent;
                selectedNote.Color = viewForm.NoteColor;
                LoadNotes();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (notesListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a note to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            StickyNote selectedNote = (StickyNote)notesListView.SelectedItems[0].Tag;
            if (MessageBox.Show($"Are you sure you want to delete the note '{selectedNote.Title}'?",
                                "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Notes.Remove(selectedNote);
                LoadNotes();
            }
        }
    }
}
