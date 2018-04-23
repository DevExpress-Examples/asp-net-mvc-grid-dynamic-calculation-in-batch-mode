# GridView - Batch Edit - How to calculate values on the fly


This example demonstrates how to create an unbound column that calculates the sum of other columns and changes its values on the fly when end-user changes any grid values using Batch edit mode. <br>
<p>You can find detailed steps by clicking below the "Show Implementation Details" link .</p>
<p><strong><br>See Also:<br></strong><a href="https://www.devexpress.com/Support/Center/p/T124151">GridView - Batch Edit - How to calculate unbound column and total summary values on the fly</a> <br><br><strong>ASP.NET Web Forms Example:</strong><br><a href="https://www.devexpress.com/Support/Center/p/T114539">ASPxGridView - Batch Edit - How to calculate values on the fly</a><br><br></p>


<h3>Description</h3>

Starting with v16.1, it's possible to modify cells without highlighting using the corresponding&nbsp;<a href="https://documentation.devexpress.com/#AspNet/DevExpressWebScriptsASPxClientGridViewBatchEditApi_SetCellValuetopic">ASPxClientGridViewBatchEditApi.SetCellValue</a>&nbsp;method overload and make a column non-editable for a user using the&nbsp;<a href="https://documentation.devexpress.com/#AspNet/DevExpressWebGridDataColumnSettings_ShowEditorInBatchEditModetopic">GridDataColumnSettings.ShowEditorInBatchEditMode</a>&nbsp;property.<br><br>To implement the required task, perform the following steps:<br><br>1. Create an unbound column in the same manner as described in the <a href="https://documentation.devexpress.com/#AspNet/CustomDocument16859">Unbound Columns</a>&nbsp;help article:<br>
<code lang="cs">settings.Columns.Add(column =&gt;{
	column.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
	column.FieldName = "Sum";
	column.ReadOnly = true;
    column.Settings.ShowEditorInBatchEditMode = false;
});

settings.CustomUnboundColumnData = (sender, e) =&gt;
{
	if (e.Column.FieldName == "Sum") {
		decimal price = Convert.ToDecimal(e.GetListSourceFieldValue("Price"));
		int quantity = Convert.ToInt32(e.GetListSourceFieldValue("Quantity"));
		e.Value = price * quantity;
	}
};</code>
<p>&nbsp;</p>
<p>&nbsp;2. Handle the&nbsp;<a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_BatchEditEndEditingtopic">ASPxClientGridView.BatchEditEndEditing</a>&nbsp;event to re-calculate the values based on the new changes and set it to the unbound column using the&nbsp;<a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridViewBatchEditApi_SetCellValuetopic">ASPxClientGridViewBatchEditApi.SetCellValue</a>&nbsp;method:</p>
<code lang="js">function OnBatchEditEndEditing(s, e) {
    var PriceColIndex = s.GetColumnByField("Price").index;
    var QuantityColIndex = s.GetColumnByField("Quantity").index;
    var priceValue = e.rowValues[PriceColIndex].value;
    var quantityValue = e.rowValues[QuantityColIndex].value;
    s.batchEditApi.SetCellValue(e.visibleIndex, "Sum", priceValue * quantityValue, null, true);
}</code>

<br/>


