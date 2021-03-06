﻿using Eto.Mac.Forms.Controls;
using TsinghuaNet.Eto.Controls;

namespace TsinghuaNet.Eto.Mac.Controls
{
    public class SortableGridColumnHandler : GridColumnHandler, SortableGridColumn.IHandler
    {
        private string headerText;
        string SortableGridColumn.IHandler.HeaderText
        {
            get => headerText;
            set
            {
                headerText = value;
                ((SortableGridColumn)Widget).RefreshBaseHeaderText();
            }
        }

        private GridSortDirection? sortDirection;
        public GridSortDirection? SortDirection
        {
            get => sortDirection;
            set
            {
                sortDirection = value;
                ((SortableGridColumn)Widget).RefreshBaseHeaderText();
            }
        }
    }
}
