using System;
using System.Collections;
using System.Collections.Generic;

namespace Clones
{
    public class CloneVersionSystem<TValue> : ICloneVersionSystem
    {
        private readonly List<Clone<TValue>> clones = new List<Clone<TValue>> { new Clone<TValue>() };
        public string Execute(string query)
        {
            var commands = query.Split();
            var cloneIndex = int.Parse(commands[1]) - 1;

            switch (commands[0])
            {
                case "learn":
                    clones[cloneIndex].Learn((TValue)Convert.ChangeType(commands, typeof(TValue)));
                    return null;
                case "rollback":
                    clones[cloneIndex].Rollback();
                    return null;
                case "relearn":
                    clones[cloneIndex].ReLearn();
                    return null;
                case "clone":
                    clones.Add(new Clone<TValue>(clones[cloneIndex]));
                    return null;
                case "check":
                    return clones[cloneIndex].Check();
                default:
                    return null;
            }
        }
    }

    public class Clone<TValue>
    {
        private readonly Stack<TValue> learnedPrograms;
        private readonly Stack<TValue> rollBackHistory;

        public Clone()
        {
            learnedPrograms = new Stack<TValue>();
            rollBackHistory = new Stack<TValue>();
        }

        public void Learn(TValue number)
        {
            rollBackHistory.Clear();
            learnedPrograms.Push(number);
        }

        public void Rollback()
        {
            rollBackHistory.Push(learnedPrograms.Pop());
        }

        public void ReLearn()
        {
            learnedPrograms.Push(rollBackHistory.Pop());
        }

        public Clone(Clone<TValue> clone)
        {
            learnedPrograms = new Stack<TValue>(clone.learnedPrograms);
            rollBackHistory = new Stack<TValue>(clone.rollBackHistory);
        }

        public string Check() => learnedPrograms.IsEmpty() ? "basic" : learnedPrograms.Peek().ToString();
    }

    public class Stack<T>
    {
        private StackItem<T> lastItem;

        public Stack() { }

        public Stack(Stack<T> stack) => lastItem = stack.lastItem;

        public T Pop()
        {
            var value = lastItem.Value;
            lastItem = lastItem.PreviousItem;
            return value;
        }

        public void Push(T value) => lastItem = new StackItem<T>(lastItem, value);
        public T Peek() => lastItem.Value;
        public bool IsEmpty() => lastItem is null;
        public void Clear() => lastItem = null;
    }

    public class StackItem<T>
    {
        public readonly StackItem<T> PreviousItem;
        public readonly T Value;

        public StackItem(StackItem<T> previousItem, T value)
        {
            PreviousItem = previousItem;
            Value = value;
        }
    }
}