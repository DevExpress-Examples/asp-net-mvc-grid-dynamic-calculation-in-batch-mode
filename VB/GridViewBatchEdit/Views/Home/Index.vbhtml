<script type="text/javascript">
    function OnBatchEditEndEditing(s, e) {
		var PriceColIndex = s.GetColumnByField("Price").index;
        var QuantityColIndex = s.GetColumnByField("Quantity").index;
        var priceValue = e.rowValues[PriceColIndex].value;
        var quantityValue = e.rowValues[QuantityColIndex].value;
        s.batchEditApi.SetCellValue(e.visibleIndex, "Sum", priceValue * quantityValue, null, true);
    }
</script>
@Html.Action("GridViewPartial")