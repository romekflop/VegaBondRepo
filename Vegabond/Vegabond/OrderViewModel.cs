using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Vegabond
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool isMatam;
        public bool IsMatam
        {
            get
            {
                return isMatam;
            }
            set
            {
                if(value != isMatam)
                {
                    isMatam = value;
                    Output = ToString();
                }
            }
        }
        private string customerName;
        public string CustomerName
        {
            get
            {
                return customerName;
            }
            set
            {
                if(value!=customerName)
                {
                    customerName = value;
                    Output = ToString();
                }
            }
        }

        private string comments;
        public string Comments
        {
            get
            {
                return comments;
            }
            set
            {
                if (value != comments)
                {
                    comments = value;
                    Output = ToString();
                }
            }
        }

        public DateTime Time
        {
            get
            {
                return DateTime.Now;
            }
        }


        private readonly IList<string> content = new List<string>
        {
        };


        private string output = "output text";
        public string Output
        {
            get
            {
                return output;
            }
            set
            {
                output = value;
                NotifyPropertyChanged();
            }
        }

        public void AddContent_SameLine(string content)
        {
            this.content[this.content.Count - 1] = $"{this.content[this.content.Count - 1]} {content}";
            Output = ToString();
        }
        public void AddContent(string content)
        {
            this.content.Add(content);
            Output = ToString();
        }

        public void DeleteLastLine()
        {
            if (content.Count == 0)
                return;

            content.RemoveAt(content.Count - 1);
            Output = ToString();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Time}");
            sb.AppendLine($"{CustomerName}");
            sb.AppendLine($"{Comments}");
            if(isMatam)
                sb.AppendLine($"מתם");
            sb.AppendLine();
            foreach (var line in content)
            {
                sb.AppendLine(line);
            }
            return sb.ToString();
        }
    }

}
