using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace StickyNotesApp
{
    public partial class MainForm: Form
    {
        private List<StickyNote> notes = new List<StickyNote>();
        private string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "StickyNotes.xml");

        public MainForm()
        {
            InitializeComponent();
            LoadNotes();
        }

        private void InitializeComponent()
        {
            this.Text = "Sticky Notes";
            this.Size = new Size(300, 200);
            this.StartPosition = FormStartPosition.CenterScreen;

            Button newNoteButton = new Button
            {
                Text = "New Note",
                Location = new Point(10, 10),
                Size = new Size(100, 30)
            };
            newNoteButton.Click += NewNoteButton_Click;

            Button viewNotesButton = new Button
            {
                Text = "View Notes",
                Location = new Point(120, 10),
                Size = new Size(100, 30)
            };
            viewNotesButton.Click += ViewNotesButton_Click;

            this.Controls.Add(newNoteButton);
            this.Controls.Add(viewNotesButton);

            this.FormClosing += MainForm_FormClosing;
        }

        private void NewNoteButton_Click(object sender, EventArgs e)
        {
            StickyNoteForm noteForm = new StickyNoteForm();
            if (noteForm.ShowDialog() == DialogResult.OK)
            {
                StickyNote note = new StickyNote
                {
                    Title = noteForm.NoteTitle,
                    Content = noteForm.NoteContent,
                    Color = noteForm.NoteColor,
                    CreatedDate = DateTime.Now
                };
                notes.Add(note);
                SaveNotes();
                MessageBox.Show("Note saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ViewNotesButton_Click(object sender, EventArgs e)
        {
            if (notes.Count == 0)
            {
                MessageBox.Show("No notes found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            NotesListForm listForm = new NotesListForm(notes);
            if (listForm.ShowDialog() == DialogResult.OK)
            {
                notes = listForm.Notes;
                SaveNotes();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveNotes();
        }

        private void SaveNotes()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<StickyNote>));
                using (TextWriter writer = new StreamWriter(savePath))
                {
                    serializer.Serialize(writer, notes);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving notes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadNotes()
        {
            if (!File.Exists(savePath))
                return;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<StickyNote>));
                using (TextReader reader = new StreamReader(savePath))
                {
                    notes = (List<StickyNote>)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading notes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
