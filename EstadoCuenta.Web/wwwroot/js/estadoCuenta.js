$(document).ready(function () {
    $.ajax({
        url: "https://localhost:7264/api/TarjetaCredito/1",
        method: "GET",
        dataType: "json",
        success: function (data) {
            // Actualizar vista con datos de la tarjeta
            $("#titular").text(data.titular);
            $("#numeroTarjeta").text(data.numeroTarjeta);
            $("#saldoActual").text(data.saldoActual.toFixed(2));
            $("#saldoDisponible").text(data.limiteCredito - data.saldoActual.toFixed(2));
            // Totales
            $("#totalMesActual").text(data.totalMesActual.toFixed(2));
            $("#totalMesAnterior").text(data.totalMesAnterior.toFixed(2));
            // Calculos
            $("#interesBonificable").text((data.saldoActual * 0.25).toFixed(2));
            $("#cuotaMinimaPagar").text((data.saldoActual * 0.05).toFixed(2));
            $("#montoTotalPagar").text(data.saldoActual.toFixed(2));
            $("#pagoContado").text((data.saldoActual + (data.saldoActual * 0.25)).toFixed(2));
            // Tabla de compras
            let tbody = $("#compras");
            tbody.empty();
            $.each(data.compras, function (index, compra) {
                let row = `<tr class="border-t">
                    <td class="p-3">${new Date(compra.fecha).toLocaleDateString()}</td>
                    <td class="p-3">${compra.descripcion}</td>
                    <td class="p-3 text-red-600">$${compra.monto.toFixed(2)}</td>
                </tr>`;
                tbody.append(row);
            });
        },
        error: function (xhr, status, error) {
            console.error("Error:", error);
        }
    })
});