﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LarsWM.Domain.Common.Models
{
    public class Container
    {
        public virtual int Height { get; set; }
        public virtual int Width { get; set; }
        public virtual int X { get; set; }
        public virtual int Y { get; set; }
        public Container Parent { get; set; } = null;
        public List<Container> Children { get; set; } = new List<Container>();

        public Container AddChild(Container container)
        {
            Children.Add(container);
            container.Parent = this;
            return container;
        }

        public Container[] AddChildren(params Container[] containers)
        {
            return containers.Select(AddChild).ToArray();
        }

        public bool RemoveChild(Container node)
        {
            node.Parent = null;
            return Children.Remove(node);
        }

        // Not sure if needed.
        public void Traverse(Action<Container> action)
        {
            action(this);
            foreach (var child in Children)
                child.Traverse(action);
        }

        public IEnumerable<Container> Flatten()
        {
            return new[] { this }.Concat(Children.SelectMany(x => x.Flatten()));
        }
    }
}