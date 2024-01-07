using Godot;
using System;
using Prism.Events;

public partial class EventAggregatorService : Node
{
    private static IEventAggregator _aggregator { get; set; }

    public override void _EnterTree()
    {
        _aggregator = new EventAggregator();
    }

    public static IEventAggregator EventAggregator()
    {
        if (Engine.IsEditorHint())
        {
            return new EditorEventAggregator();
        }
        else
        {
            return _aggregator;
        }

    }
}

class EditorEventAggregator : IEventAggregator
{
    public TEventType GetEvent<TEventType>() where TEventType : EventBase, new()
    {
        return new TEventType();
    }
}
