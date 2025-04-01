using System.Drawing;
using System.Xml.Serialization;
using System;

namespace StickyNotesApp
{
    [Serializable]
    public class StickyNote
    {
        public string Title { get; set; }
        public string Content { get; set; }
        [XmlIgnore]
        public Color Color { get; set; }

        // Used for XML serialization since Color isn't directly serializable
        public string ColorName
        {
            get { return ColorTranslator.ToHtml(Color); }
            set { Color = ColorTranslator.FromHtml(value); }
        }

        public DateTime CreatedDate { get; set; }

        public StickyNote()
        {
            // Default values
            Title = string.Empty;
            Content = string.Empty;
            Color = Color.LightYellow;
            CreatedDate = DateTime.Now;
        }

        public StickyNote(string title, string content, Color color, string colorName, DateTime createdDate)
        {
            Title = title;
            Content = content;
            Color = color;
            ColorName = colorName;
            CreatedDate = createdDate;
        }
    }
}