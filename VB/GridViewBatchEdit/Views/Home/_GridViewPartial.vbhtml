@Code
	Dim grid = Html.DevExpress().GridView(Sub(settings)
		settings.Name = "GridView"
		settings.CallbackRouteValues = New With {Key .Controller = "Home", Key .Action = "GridViewPartial"}
		settings.SettingsEditing.BatchUpdateRouteValues = New With {Key .Controller = "Home", Key .Action = "BatchUpdatePartial"}
		settings.SettingsEditing.Mode = GridViewEditingMode.Batch
		settings.CommandColumn.Visible = True
		settings.CommandColumn.ShowDeleteButton = True
		settings.CommandColumn.ShowNewButtonInHeader = True
		settings.KeyFieldName = "ID"
		settings.ClientSideEvents.BatchEditStartEditing = "OnBatchEditStartEditing"
		settings.ClientSideEvents.BatchEditEndEditing = "OnBatchEditEndEditing"
		settings.Columns.Add(Sub(column)
			column.FieldName = "Quantity"
			column.ColumnType = MVCxGridViewColumnType.SpinEdit
		End Sub)
		settings.Columns.Add(Sub(column)
			column.FieldName = "Price"
			column.ColumnType = MVCxGridViewColumnType.SpinEdit
		End Sub)
		settings.Columns.Add(Sub(column)
			column.UnboundType = DevExpress.Data.UnboundColumnType.Decimal
			column.FieldName = "Sum"
			column.ReadOnly = True
			column.SetEditItemTemplateContent(Sub(c)
			End Sub)
		End Sub)
		settings.CustomUnboundColumnData = Sub(sender, e)
			If e.Column.FieldName = "Sum" Then
				Dim price As Decimal = Convert.ToDecimal(e.GetListSourceFieldValue("Price"))
				Dim quantity As Integer = Convert.ToInt32(e.GetListSourceFieldValue("Quantity"))
				e.Value = price * quantity
			End If
		End Sub
		settings.CellEditorInitialize = Sub(s, e)
			Dim editor As ASPxEdit = CType(e.Editor, ASPxEdit)
			editor.ValidationSettings.Display = Display.Dynamic
		End Sub
		settings.HtmlDataCellPrepared = Sub(sender, e)
			If e.DataColumn.FieldName = "Sum" Then
				e.Cell.Attributes.Add("onclick", "event.cancelBubble = true")
			End If
		End Sub
	End Sub)
	If ViewData("EditError") IsNot Nothing Then
		grid.SetEditErrorText(CStr(ViewData("EditError")))
    End If
End Code
@grid.Bind(Model).GetHtml()