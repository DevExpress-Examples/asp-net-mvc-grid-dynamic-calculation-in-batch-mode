<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128549548/14.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T124603)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [HomeController.cs](./CS/GridViewBatchEdit/Controllers/HomeController.cs) (VB: [HomeController.vb](./VB/GridViewBatchEdit/Controllers/HomeController.vb))
* [Model.cs](./CS/GridViewBatchEdit/Models/Model.cs) (VB: [Model.vb](./VB/GridViewBatchEdit/Models/Model.vb))
* [_GridViewPartial.cshtml](./CS/GridViewBatchEdit/Views/Home/_GridViewPartial.cshtml)
* [Index.cshtml](./CS/GridViewBatchEdit/Views/Home/Index.cshtml)
<!-- default file list end -->
# GridView - Batch Edit - How to calculate values on the fly
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/128549548/)**
<!-- run online end -->


This example demonstrates how to create an unbound column that calculates the sum of other columns and changes its values on the fly when end-user changes any grid values using Batch edit mode. <br>
<p>You can find detailed steps by clicking below the "Show Implementation Details" link .</p>
<p><strong><br>See Also:<br></strong><a href="https://www.devexpress.com/Support/Center/p/T124151">GridView - Batch Edit - How to calculate unbound column and total summary values on the fly</a> <br><br><strong>ASP.NET Web Forms Example:</strong><br><a href="https://www.devexpress.com/Support/Center/p/T114539">ASPxGridView - Batch Edit - How to calculate values on the fly</a><br><br></p>


<h3>Description</h3>

To implement the required task, perform the following steps:<br><br>1. Create an unbound column in the same manner as described in the <a href="https://documentation.devexpress.com/#AspNet/CustomDocument16859">Unbound Columns</a>&nbsp;help article:<br>
<code lang="cs">settings.Columns.Add(column =&gt;
{
	column.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
	column.FieldName = "Sum";
	column.ReadOnly = true;
});

settings.CustomUnboundColumnData = (sender, e) =&gt;
{
	if (e.Column.FieldName == "Sum") {
		decimal price = Convert.ToDecimal(e.GetListSourceFieldValue("Price"));
		int quantity = Convert.ToInt32(e.GetListSourceFieldValue("Quantity"));
		e.Value = price * quantity;
	}
};</code>
<p>2. Handle the HtmlDataCellPrepared&nbsp;event to cancel editing for the Sum column:</p>
<pre class="cr-code"> settings.HtmlDataCellPrepared = (sender, e) =&gt;
 {
    if (e.DataColumn.FieldName == "Sum")
       e.Cell.Attributes.Add("onclick", "event.cancelBubble = true");
 };</pre>
<p>&nbsp;3. Handle the&nbsp;<a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_BatchEditEndEditingtopic">ASPxClientGridView.BatchEditEndEditing</a>&nbsp;event to re-calculate the values based on the new changes and set it to the unbound column using the&nbsp;<a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridViewBatchEditApi_SetCellValuetopic">ASPxClientGridViewBatchEditApi.SetCellValue</a>&nbsp;method:</p>
<code lang="js">function OnBatchEditEndEditing(s, e) {
    window.setTimeout(function () {
        var price = s.batchEditApi.GetCellValue(e.visibleIndex, "Price");
        var quantity = s.batchEditApi.GetCellValue(e.visibleIndex, "Quantity");
        s.batchEditApi.SetCellValue(e.visibleIndex, "Sum", price * quantity);
    }, 10);
}</code>

<br/>


