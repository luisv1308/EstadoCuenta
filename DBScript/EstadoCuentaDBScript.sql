USE [EstadoCuentaDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 17/3/2025 14:46:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TarjetasCredito]    Script Date: 17/3/2025 14:46:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TarjetasCredito](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titular] [nvarchar](100) NOT NULL,
	[NumeroTarjeta] [nvarchar](16) NOT NULL,
	[FechaVencimiento] [datetime2](7) NOT NULL,
	[SaldoActual] [decimal](18, 2) NOT NULL,
	[LimiteCredito] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_TarjetasCredito] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transacciones]    Script Date: 17/3/2025 14:46:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transacciones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TarjetaCreditoId] [int] NOT NULL,
	[Descripcion] [nvarchar](500) NOT NULL,
	[Monto] [decimal](18, 2) NOT NULL,
	[Fecha] [datetime2](7) NOT NULL,
	[Tipo] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Transacciones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250311232837_InitialCreate', N'6.0.25')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250317014543_NuevaMigracion', N'6.0.25')
GO
SET IDENTITY_INSERT [dbo].[TarjetasCredito] ON 

INSERT [dbo].[TarjetasCredito] ([Id], [Titular], [NumeroTarjeta], [FechaVencimiento], [SaldoActual], [LimiteCredito]) VALUES (1, N'Luis Enrique Velasquez Gomez', N'1111222233334444', CAST(N'2028-12-31T00:00:00.0000000' AS DateTime2), CAST(1000.00 AS Decimal(18, 2)), CAST(10000.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[TarjetasCredito] OFF
GO
SET IDENTITY_INSERT [dbo].[Transacciones] ON 

INSERT [dbo].[Transacciones] ([Id], [TarjetaCreditoId], [Descripcion], [Monto], [Fecha], [Tipo]) VALUES (1, 1, N'Compra de sal', CAST(5.00 AS Decimal(18, 2)), CAST(N'2025-03-17T00:00:00.0000000' AS DateTime2), N'Compra')
INSERT [dbo].[Transacciones] ([Id], [TarjetaCreditoId], [Descripcion], [Monto], [Fecha], [Tipo]) VALUES (2, 1, N'Compra viveres', CAST(50.00 AS Decimal(18, 2)), CAST(N'2025-03-17T00:00:00.0000000' AS DateTime2), N'Compra')
INSERT [dbo].[Transacciones] ([Id], [TarjetaCreditoId], [Descripcion], [Monto], [Fecha], [Tipo]) VALUES (3, 1, N'Compra de comida', CAST(10.00 AS Decimal(18, 2)), CAST(N'2025-02-17T00:00:00.0000000' AS DateTime2), N'Compra')
INSERT [dbo].[Transacciones] ([Id], [TarjetaCreditoId], [Descripcion], [Monto], [Fecha], [Tipo]) VALUES (4, 1, N'Pago a tarjeta', CAST(50.00 AS Decimal(18, 2)), CAST(N'2025-03-17T00:00:00.0000000' AS DateTime2), N'Pago')
INSERT [dbo].[Transacciones] ([Id], [TarjetaCreditoId], [Descripcion], [Monto], [Fecha], [Tipo]) VALUES (5, 1, N'Compra viveres', CAST(25.00 AS Decimal(18, 2)), CAST(N'2025-03-17T00:00:00.0000000' AS DateTime2), N'Compra')
INSERT [dbo].[Transacciones] ([Id], [TarjetaCreditoId], [Descripcion], [Monto], [Fecha], [Tipo]) VALUES (6, 1, N'Pago a tarjeta', CAST(10.00 AS Decimal(18, 2)), CAST(N'2025-03-17T00:00:00.0000000' AS DateTime2), N'Pago')
INSERT [dbo].[Transacciones] ([Id], [TarjetaCreditoId], [Descripcion], [Monto], [Fecha], [Tipo]) VALUES (7, 1, N'Pago a tarjeta', CAST(5.00 AS Decimal(18, 2)), CAST(N'2025-03-17T00:00:00.0000000' AS DateTime2), N'Pago')
SET IDENTITY_INSERT [dbo].[Transacciones] OFF
GO
ALTER TABLE [dbo].[Transacciones]  WITH CHECK ADD  CONSTRAINT [FK_Transacciones_TarjetasCredito_TarjetaCreditoId] FOREIGN KEY([TarjetaCreditoId])
REFERENCES [dbo].[TarjetasCredito] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transacciones] CHECK CONSTRAINT [FK_Transacciones_TarjetasCredito_TarjetaCreditoId]
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerEstadoCuenta]    Script Date: 17/3/2025 14:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ObtenerEstadoCuenta]
    @TarjetaId INT,
    @PorcentajeInteres DECIMAL(5,2) = 0.25,
    @PorcentajeSaldoMinimo DECIMAL(5,2) = 0.05
AS
BEGIN
    SET NOCOUNT ON;

    -- Definir variables para evitar cálculos repetidos
    DECLARE @MesActual INT = MONTH(GETDATE());
    DECLARE @AnioActual INT = YEAR(GETDATE());
    DECLARE @MesAnterior INT = MONTH(DATEADD(MONTH, -1, GETDATE()));
    DECLARE @AnioAnterior INT = YEAR(DATEADD(MONTH, -1, GETDATE()));

    -- Variables para almacenar los totales de transacciones
    DECLARE @TotalMesActual DECIMAL(10,2) = 0;
    DECLARE @TotalMesAnterior DECIMAL(10,2) = 0;

    -- Obtener los montos totales de compras agrupados por mes
    SELECT
        @TotalMesActual = ISNULL(SUM(CASE WHEN MONTH(Fecha) = @MesActual AND YEAR(Fecha) = @AnioActual THEN Monto ELSE 0 END), 0),
        @TotalMesAnterior = ISNULL(SUM(CASE WHEN MONTH(Fecha) = @MesAnterior AND YEAR(Fecha) = @AnioAnterior THEN Monto ELSE 0 END), 0)
    FROM Transacciones
    WHERE TarjetaCreditoId = @TarjetaId AND Tipo = 'Compra';

    -- Obtener los datos de la tarjeta con cálculos optimizados
    SELECT
        t.Id AS TarjetaId,
        t.Titular,
        t.NumeroTarjeta,
        CAST(t.SaldoActual AS DECIMAL(10,2)) AS SaldoActual,
        CAST(t.LimiteCredito AS DECIMAL(10,2)) AS LimiteCredito,
        CAST((t.LimiteCredito - t.SaldoActual) AS DECIMAL(10,2)) AS SaldoDisponible, 
        CAST(t.SaldoActual * @PorcentajeInteres AS DECIMAL(10,2)) AS InteresBonificable,
        CAST(t.SaldoActual * @PorcentajeSaldoMinimo AS DECIMAL(10,2)) AS CuotaMinimaPagar,
        CAST(t.SaldoActual + @TotalMesActual AS DECIMAL(10,2)) AS MontoTotalPagar,
        CAST((t.SaldoActual + @TotalMesActual) + (t.SaldoActual * @PorcentajeInteres) AS DECIMAL(10,2)) AS PagoContadoConIntereses,
        @TotalMesActual AS TotalMesActual,
        @TotalMesAnterior AS TotalMesAnterior
    FROM TarjetasCredito t
    WHERE t.Id = @TarjetaId;

    -- Obtener la lista de compras del mes actual
    SELECT 
        tr.Fecha,
        tr.Descripcion,
        CAST(tr.Monto AS DECIMAL(10,2)) AS Monto,
        tr.Tipo
    FROM Transacciones tr
    WHERE tr.TarjetaCreditoId = @TarjetaId
    AND tr.Tipo = 'Compra'
    AND MONTH(tr.Fecha) = @MesActual
    AND YEAR(tr.Fecha) = @AnioActual
    ORDER BY tr.Fecha DESC, tr.Id DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerTransaccionesPorTarjeta]    Script Date: 17/3/2025 14:46:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ObtenerTransaccionesPorTarjeta]
	@TarjetaId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		Id,
		TarjetaCreditoId,
		Descripcion,
		Monto,
		Fecha,
		Tipo
	FROM Transacciones
	WHERE TarjetaCreditoId = @TarjetaId
	AND MONTH(Fecha) = MONTH(GETDATE())
	AND Year(Fecha) = YEAR(GETDATE())
	ORDER BY Fecha DESC;
END;
GO
