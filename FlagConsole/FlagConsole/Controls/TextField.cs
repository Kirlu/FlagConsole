﻿using System;

namespace FlagConsole.Controls
{
    /// <summary>
    /// Provides a text field where the user can do an input
    /// </summary>
    public class TextField : Control, IFocusable
    {
        /// <summary>
        /// Gets the input.
        /// </summary>
        /// <value>
        /// The input.
        /// </value>
        public virtual string Text { get; private set; }

        /// <summary>
        /// Gets or sets the length of the text field.
        /// </summary>
        /// <value>
        /// The length of the text field.
        /// </value>
        public virtual int Length { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="IFocusable"/> is focused.
        /// </summary>
        /// <value>
        /// true if focused; otherwise, false.
        /// </value>
        public virtual bool IsFocused { get; set; }

        /// <summary>
        /// Occurs when the input has been entered.
        /// </summary>
        public event EventHandler TextEntered;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextField"/> class.
        /// </summary>
        public TextField()
        {
            this.Text = String.Empty;
        }

        /// <summary>
        /// Focuses the control and executes it's behaviour (e.g the selection of a menu or the input of a textfield)
        /// </summary>
        public void Focus()
        {
            this.IsFocused = true;
            this.IsVisible = true;
            this.ScanInput();
        }

        /// <summary>
        /// Defocuses the control and stopps it's behaviour.
        /// </summary>
        public void Defocus()
        {
            this.IsFocused = false;
        }

        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            System.Console.BackgroundColor = ConsoleColor.White;
            System.Console.ForegroundColor = ConsoleColor.Black;

            string background = String.Empty;
            background = background.PadRight(this.Length, ' ');

            System.Console.SetCursorPosition(this.AbsoluteLocation.X, this.AbsoluteLocation.Y);
            System.Console.Write(background);

            System.Console.SetCursorPosition(this.AbsoluteLocation.X, this.AbsoluteLocation.Y);
            System.Console.Write(this.Text);

            Console.ResetColor();
        }

        /// <summary>
        /// Raises the <see cref="E:TextEntered"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnTextEntered(EventArgs e)
        {
            if (this.TextEntered != null)
            {
                this.TextEntered(this, e);
            }
        }

        /// <summary>
        /// Scans the input.
        /// </summary>
        protected virtual void ScanInput()
        {
            ConsoleKeyInfo key;

            do
            {
                this.Update();

                key = System.Console.ReadKey(true);

                if (key.Key != ConsoleKey.Enter)
                {
                    if (key.Key == ConsoleKey.Backspace)
                    {
                        if (this.Text.Length > 0)
                        {
                            this.Text = this.Text.Substring(0, this.Text.Length - 1);
                        }
                    }

                    else if (this.Text.Length < this.Length)
                    {
                        this.Text += key.KeyChar.ToString();
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter && this.IsVisible);

            this.OnTextEntered(EventArgs.Empty);
        }
    }
}