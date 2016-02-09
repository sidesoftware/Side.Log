
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ConsoleLog
{
    public partial class ConsoleLog: UserControl
    {
        private string _headerDivider;
        private readonly RichTextBox _textBox;

        public ConsoleLog()
        {
            InitializeComponent();

            _textBox = textBox;

            ConsoleBackColor = Color.FromArgb(51, 51, 51);
            ConsoleFont = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            StandardForeColor = Color.FromArgb(225, 213, 112);
            StandardBackColor = Color.FromArgb(51, 51, 51);
            InfoForeColor = Color.FromArgb(99, 211, 234);
            InfoBackColor = Color.FromArgb(51, 51, 51);
            WarningForeColor = Color.FromArgb(51, 51, 51);
            WarningBackColor = Color.FromArgb(255, 252, 0);
            ErrorForeColor = Color.White;
            ErrorBackColor = Color.Tomato;
            SuccessForeColor = Color.FromArgb(163, 233, 45);
            SuccessBackColor = Color.FromArgb(51, 51, 51);
            StandoutForeColor = Color.FromArgb(250, 1, 194);
            StandoutBackColor = Color.FromArgb(51, 51, 51);
            SubtleForeColor = Color.FromArgb(156, 156, 156);
            SubtleBackColor = Color.FromArgb(51, 51, 51);
            EventForeColor = Color.FromArgb(78, 201, 176);
            EventBackColor = Color.FromArgb(51, 51, 51);
            EventWarningForeColor = Color.White;
            EventWarningBackColor = Color.FromArgb(78, 201, 176);

        }

        #region Public Properties

        [Description("Header divider characters"), Category("Console Log")]
        public string HeaderDivider
        {
            get
            {
                return string.IsNullOrEmpty(_headerDivider) ? "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++" : _headerDivider;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _headerDivider = value;
            }
        }

        [Description("Total lines in the console"), Category("Console Log")]
        public long TotalLines
        {
            get { return textBox.Lines.LongCount(); }
        }


        [Description("Console back color"), Category("Console Log")]
        public Color ConsoleBackColor
        {
            get { return _textBox.BackColor; }
            set { _textBox.BackColor = value; }
        }

        [Description("Console back color"), Category("Console Log")]
        public Font ConsoleFont
        {
            get { return _textBox.Font; }
            set { _textBox.Font = value; }
        }

        [Description("Default fore color"), Category("Console Log")]
        public Color StandardForeColor { get; set; }

        [Description("Default Back color"), Category("Console Log")]
        public Color StandardBackColor { get; set; }

        [Description("Info fore color"), Category("Console Log")]
        public Color InfoForeColor { get; set; }

        [Description("Info Back color"), Category("Console Log")]
        public Color InfoBackColor { get; set; }

        [Description("Warning fore color"), Category("Console Log")]
        public Color WarningForeColor { get; set; }

        [Description("Warning back color"), Category("Console Log")]
        public Color WarningBackColor { get; set; }

        [Description("Error fore color"), Category("Console Log")]
        public Color ErrorForeColor { get; set; }

        [Description("Error back color"), Category("Console Log")]
        public Color ErrorBackColor { get; set; }

        [Description("Success fore color"), Category("Console Log")]
        public Color SuccessForeColor { get; set; }

        [Description("Success back color"), Category("Console Log")]
        public Color SuccessBackColor { get; set; }

        [Description("Standout fore color"), Category("Console Log")]
        public Color StandoutForeColor { get; set; }

        [Description("Standout back color"), Category("Console Log")]
        public Color StandoutBackColor { get; set; }

        [Description("Subtle fore color"), Category("Console Log")]
        public Color SubtleForeColor { get; set; }

        [Description("Subtle back color"), Category("Console Log")]
        public Color SubtleBackColor { get; set; }

        [Description("Event fore color"), Category("Console Log")]
        public Color EventForeColor { get; set; }

        [Description("Event back color"), Category("Console Log")]
        public Color EventBackColor { get; set; }

        [Description("Event Warning fore color"), Category("Console Log")]
        public Color EventWarningForeColor { get; set; }

        [Description("Event Warning back color"), Category("Console Log")]
        public Color EventWarningBackColor { get; set; }

        #endregion

        #region Public Functions

        public void Log(LogStatus x, bool verbose = false)
        {
            switch (x.LogType)
            {
                case LogTypeEnum.Default:
                    Log(x.Message);
                    break;
                case LogTypeEnum.Subtle:
                    if (verbose) LogSubtle(x.Message);
                    break;
                case LogTypeEnum.Info:
                    LogInfo(x.Message);
                    break;
                case LogTypeEnum.Success:
                    if (verbose) LogSuccess(x.Message);
                    break;
                case LogTypeEnum.Warning:
                    if (verbose) LogWarning(x.Message);
                    break;
                case LogTypeEnum.Standout:
                    LogStandout(x.Message);
                    break;
                case LogTypeEnum.Error:
                    LogError(x.Message);
                    if (x.Exception != null) LogError(x.Exception.ToString());
                    break;
                case LogTypeEnum.Debug:
                    if (verbose) LogSubtle(x.Message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Refreshes the log
        /// </summary>
        public override void Refresh()
        {
            _textBox.Refresh();
        }

        /// <summary>
        /// Saves the contents of the log to a text file
        /// </summary>
        /// <param name="path">The path where the file should be saved</param>
        public void SaveFile(string path)
        {
            _textBox.SaveFile(path);
        }

        /// <summary>
        /// Saves the contents of the log to a text file
        /// </summary>
        /// <param name="path">The path where the file should be saved</param>
        /// <param name="fileType">The stream type</param>
        public void SaveFile(string path, RichTextBoxStreamType fileType)
        {
            _textBox.SaveFile(path, fileType);
        }

        /// <summary>
        /// Saves the contents of the log to a text file
        /// </summary>
        /// <param name="data">The stream of the data to save</param>
        /// <param name="fileType">The stream type</param>
        public void SaveFile(Stream data, RichTextBoxStreamType fileType)
        {
            _textBox.SaveFile(data, fileType);
        }

        /// <summary>
        /// Clears the contents of the log
        /// </summary>
        public void ClearContents()
        {
            textBox.Clear();
        }

        public void LogHeader(string message)
        {
            Log(HeaderDivider, true);
            Log(message);
            Log(HeaderDivider, false, true);
        }

        /// <summary>
        /// Helper method to log an info message
        /// </summary>
        /// <param name="message">The message or value to log</param>
        public void LogInfo(object message)
        {
            LogInfo(message.ToString(), null);
        }

        /// <summary>
        /// Helper method to log an info message by replacing the format item in a specified string with the string representation of a corresponding object in a specified array.
        /// </summary>
        /// <param name="message">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="message"/> or <paramref name="args"/> is null. </exception><exception cref="T:System.FormatException"><paramref name="message"/> is invalid.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args"/> array. </exception><filterpriority>1</filterpriority>
        public void LogInfo(string message, params object[] args)
        {
            // Build the message with or without args
            var formattedMessage = args == null ? message : string.Format(message, args);

            message = string.Format("{0} - {1}", DateTime.Now.ToLongTimeString(), formattedMessage);
            Log(message, InfoForeColor, InfoBackColor);
        }

        /// <summary>
        /// Helper method to log a warning message
        /// </summary>
        /// <param name="message">The message or value to log</param>
        public void LogWarning(object message)
        {
            LogWarning(message.ToString(), null);
        }

        /// <summary>
        /// Helper method to log a warning message by replacing the format item in a specified string with the string representation of a corresponding object in a specified array.
        /// </summary>
        /// <param name="message">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="message"/> or <paramref name="args"/> is null. </exception><exception cref="T:System.FormatException"><paramref name="message"/> is invalid.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args"/> array. </exception><filterpriority>1</filterpriority>
        public void LogWarning(string message, params object[] args)
        {
            // Build the message with or without args
            var formattedMessage = args == null ? message : string.Format(message, args);

            message = string.Format("{0} - {1}", DateTime.Now.ToLongTimeString(), formattedMessage);
            Log(message, WarningForeColor, WarningBackColor);
        }

        /// <summary>
        /// Helper method to log an error message
        /// </summary>
        /// <param name="message">The message or value to log</param>
        public void LogError(object message)
        {
            LogError(message.ToString(), null);
        }

        /// <summary>
        /// Helper method to log an error message by replacing the format item in a specified string with the string representation of a corresponding object in a specified array.
        /// </summary>
        /// <param name="message">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="message"/> or <paramref name="args"/> is null. </exception><exception cref="T:System.FormatException"><paramref name="message"/> is invalid.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args"/> array. </exception><filterpriority>1</filterpriority>
        public void LogError(string message, params object[] args)
        {
            // Build the message with or without args
            var formattedMessage = args == null ? message : string.Format(message, args);

            var textMessage = string.Format("{0} - {1}", DateTime.Now.ToLongTimeString(), formattedMessage);
            Log(textMessage, ErrorForeColor, ErrorBackColor);
        }

        /// <summary>
        /// Helper method to log a success message
        /// </summary>
        /// <param name="message">The message or value to log</param>
        public void LogSuccess(object message)
        {
            LogSuccess(message.ToString(), null);
        }

        /// <summary>
        /// Helper method to log a success message by replacing the format item in a specified string with the string representation of a corresponding object in a specified array.
        /// </summary>
        /// <param name="message">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="message"/> or <paramref name="args"/> is null. </exception><exception cref="T:System.FormatException"><paramref name="message"/> is invalid.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args"/> array. </exception><filterpriority>1</filterpriority>
        public void LogSuccess(string message, params object[] args)
        {
            // Build the message with or without args
            var formattedMessage = args == null ? message : string.Format(message, args);

            message = string.Format("{0} - {1}", DateTime.Now.ToLongTimeString(), formattedMessage);
            Log(message, SuccessForeColor, SuccessBackColor);
        }

        /// <summary>
        /// Helper method to log a standout message
        /// </summary>
        /// <param name="message">The message or value to log</param>
        public void LogStandout(object message)
        {
            LogStandout(message.ToString(), null);
        }

        /// <summary>
        /// Helper method to log a standout message by replacing the format item in a specified string with the string representation of a corresponding object in a specified array.
        /// </summary>
        /// <param name="message">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="message"/> or <paramref name="args"/> is null. </exception><exception cref="T:System.FormatException"><paramref name="message"/> is invalid.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args"/> array. </exception><filterpriority>1</filterpriority>
        public void LogStandout(string message, params object[] args)
        {
            // Build the message with or without args
            var formattedMessage = args == null ? message : string.Format(message, args);

            message = string.Format("{0} - {1}", DateTime.Now.ToLongTimeString(), formattedMessage);
            Log(message, StandoutForeColor, StandoutBackColor);
        }

        /// <summary>
        /// Helper method to log an subtle message
        /// </summary>
        /// <param name="message">The message or value to log</param>
        public void LogSubtle(object message)
        {
            LogSubtle(message.ToString(), null);
        }

        /// <summary>
        /// Helper method to log an subtle message by replacing the format item in a specified string with the string representation of a corresponding object in a specified array.
        /// </summary>
        /// <param name="message">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="message"/> or <paramref name="args"/> is null. </exception><exception cref="T:System.FormatException"><paramref name="message"/> is invalid.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args"/> array. </exception><filterpriority>1</filterpriority>
        public void LogSubtle(string message, params object[] args)
        {
            // Build the message with or without args
            var formattedMessage = args == null ? message : string.Format(message.ToString(), args);

            message = string.Format("{0} - {1}", DateTime.Now.ToLongTimeString(), formattedMessage);

            // Log the message
            Log(message, SubtleForeColor, SubtleBackColor);
        }

        /// <summary>
        /// Helper method to log an event message
        /// </summary>
        /// <param name="message">The message or value to log</param>
        public void LogEvent(object message)
        {
            LogEvent(message.ToString(), null);
        }

        /// <summary>
        /// Helper method to log an event message by replacing the format item in a specified string with the string representation of a corresponding object in a specified array.
        /// </summary>
        /// <param name="message">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="message"/> or <paramref name="args"/> is null. </exception><exception cref="T:System.FormatException"><paramref name="message"/> is invalid.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args"/> array. </exception><filterpriority>1</filterpriority>
        public void LogEvent(string message, params object[] args)
        {
            // Build the message with or without args
            var formattedMessage = args == null ? message : string.Format(message, args);

            message = string.Format("{0} - {1}", DateTime.Now.ToLongTimeString(), formattedMessage);

            Log(message, EventForeColor, EventBackColor);
        }

        /// <summary>
        /// Helper method to log an event warning
        /// </summary>
        /// <param name="message">The message or value to log</param>
        public void LogEventWarning(object message)
        {
            LogEventWarning(message.ToString(), null);
        }

        /// <summary>
        /// Helper method to log an event warning by replacing the format item in a specified string with the string representation of a corresponding object in a specified array.
        /// </summary>
        /// <param name="message">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="message"/> or <paramref name="args"/> is null. </exception><exception cref="T:System.FormatException"><paramref name="message"/> is invalid.-or- The index of a format item is less than zero, or greater than or equal to the length of the <paramref name="args"/> array. </exception><filterpriority>1</filterpriority>
        public void LogEventWarning(string message, params object[] args)
        {
            // Build the message with or without args
            var formattedMessage = args == null ? message : string.Format(message, args);

            // Append the date to the message
            // TODO: Perhaps have a property that can turn off the datetime stamp
            message = string.Format("{0} - {1}", DateTime.Now.ToLongTimeString(), formattedMessage);

            // Log the message
            Log(message, EventWarningForeColor, EventWarningBackColor);
        }

        /// <summary>
        /// Main function for logging a message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="spaceBefore"></param>
        /// <param name="spaceAfter"></param>
        public void Log(string message, bool spaceBefore = false, bool spaceAfter = false)
        {
            if (spaceBefore) Log("", StandardForeColor, StandardBackColor);
            Log(message, StandardForeColor, StandardBackColor);
            if (spaceAfter) Log("", StandardForeColor, StandardBackColor);
        }

        public void Log(object message, Color foreColor, Color selectionColor)
        {
            // Check if we need to invoke
            if (_textBox.InvokeRequired)
            {
                _textBox.Invoke(new MethodInvoker(() => Log(message, foreColor, selectionColor)));
                return;
            }

            // Store the current length
            var start = _textBox.TextLength;

            // Append the text
            _textBox.AppendText(message + Environment.NewLine);

            // Get the new length
            var end = _textBox.TextLength;

            // Textbox may transform chars, so (end-start) != text.Length
            _textBox.Select(start, end - start);
            {
                _textBox.SelectionColor = foreColor;
                _textBox.SelectionBackColor = selectionColor;
            }

            _textBox.SelectionLength = 0; // clear

            // Scroll to the end
            _textBox.ScrollToCaret();
        }

        #endregion
    }
}
