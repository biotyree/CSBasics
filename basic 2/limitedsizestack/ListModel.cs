using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection;

namespace TodoApplication
{
    class CommandInfo<TItem>
    {
        public TItem value { get; set; }
        public int index { get; set; }
        public string command { get; set; }
    }

    public interface ICommand<TItem>
    {
        void Execute(List<TItem> Items);
        void Undo(List<TItem> Items);
    }

    abstract class Command<TItem> : ICommand <TItem>
    {
        protected TItem Item { get; set; }
        protected int Index { get; set; }

        public abstract void Execute(List<TItem> Items);

        public abstract void Undo(List<TItem> Items);
    }

    class AddItemCom<TItem> : Command<TItem>
    {
        public AddItemCom(TItem item)
        {
            Item = item;
        }

        public override void Execute(List<TItem> Items)
        {
            Index = Items.Count;
            Items.Add(Item);
        }

        public override void Undo(List<TItem> Items)
        {
            Items.RemoveAt(Index);
        }
    }

    class RemoveCom<TItem> : Command<TItem>
    {
        public RemoveCom(int value)
        {
            Index = value;
        }

        public override void Execute(List<TItem> Items)
        {
            Item = Items[Index];
            Items.RemoveAt(Index);
        }

        public override void Undo(List<TItem> Items)
        {
            Items.Insert(Index, Item);
        }
    }

    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        public int Limit;

        LimitedSizeStack<ICommand<TItem>> commandHistory;

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            Limit = limit;
            commandHistory = new LimitedSizeStack<ICommand<TItem>>(limit);
        }

        public void AddItem(TItem item)
        {
            var command = new AddItemCom<TItem>(item);
            command.Execute(Items);

            commandHistory.Push(command);
        }

        public void RemoveItem(int index)
        {
            var command = new RemoveCom<TItem>(index);
            command.Execute(Items);

            commandHistory.Push(command);
        }


        public bool CanUndo()
        {
            return commandHistory.Count > 0;
        }

        public void Undo()
        {
            if(commandHistory.Count > 0)
            {
                var command = commandHistory.Pop();

                command.Undo(Items);
            }
        }
    }
}
