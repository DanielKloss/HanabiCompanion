﻿using System;
using System.Diagnostics;
using System.Windows.Input;

namespace HanabiCompanion.UI
{
    public class Command : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        public event EventHandler CanExecuteChanged;

        public Command(Action execute, Func<bool> canexecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canexecute ?? (() => true);
        }

        [DebuggerStepThrough]
        public bool CanExecute(object p)
        {
            try
            {
                return _canExecute();
            }
            catch { return false; }
        }

        public void Execute(object p)
        {
            if (!CanExecute(p))
                return;
            try { _execute(); }
            catch { Debugger.Break(); }
        }


        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Command<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;
        public event EventHandler CanExecuteChanged;

        public Command(Action<T> execute, Func<T, bool> canexecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canexecute ?? (e => true);
        }

        [DebuggerStepThrough]
        public bool CanExecute(object p)
        {
            try
            {
                var _Value = (T)Convert.ChangeType(p, typeof(T));
                return _canExecute == null ? true : _canExecute(_Value);
            }
            catch { return false; }
        }

        public void Execute(object p)
        {
            if (!CanExecute(p))
                return;
            var _Value = (T)Convert.ChangeType(p, typeof(T));
            _execute(_Value);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
