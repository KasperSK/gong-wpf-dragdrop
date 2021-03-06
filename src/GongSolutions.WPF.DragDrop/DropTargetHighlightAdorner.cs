﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GongSolutions.Wpf.DragDrop
{
  public class DropTargetHighlightAdorner : DropTargetAdorner
  {
    [Obsolete("This constructor is obsolete and will be deleted in next major release.")]
    public DropTargetHighlightAdorner(UIElement adornedElement)
      : base(adornedElement, (DropInfo)null)
    {
    }

    public DropTargetHighlightAdorner(UIElement adornedElement, DropInfo dropInfo)
      : base(adornedElement, dropInfo)
    {
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
      var dropInfo = this.DropInfo;
      var visualTargetItem = dropInfo.VisualTargetItem;
      if (visualTargetItem != null) {
        var rect = Rect.Empty;

        var tvItem = visualTargetItem as TreeViewItem;
        if (tvItem != null && VisualTreeHelper.GetChildrenCount(tvItem) > 0) {
          var descendant = VisualTreeHelper.GetDescendantBounds(tvItem);
          var translatePoint = tvItem.TranslatePoint(new Point(), this.AdornedElement);
          var itemRect = new Rect(translatePoint, tvItem.RenderSize);
          descendant.Union(itemRect);
          rect = new Rect(translatePoint, new Size(descendant.Width - translatePoint.X, tvItem.ActualHeight));
        }
        if (rect.IsEmpty) {
          rect = new Rect(visualTargetItem.TranslatePoint(new Point(), this.AdornedElement), VisualTreeHelper.GetDescendantBounds(visualTargetItem).Size);
        }
        drawingContext.DrawRoundedRectangle(null, new Pen(Brushes.Gray, 2), rect, 2, 2);
      }
    }
  }
}
