# **Estado de Cuenta de Tarjeta de Crédito**

## **Descripción del Proyecto**
Este proyecto consiste en una aplicación web y una API REST que permite gestionar el estado de cuenta de una tarjeta de crédito.
Incluye funcionalidades como el registro de compras y pagos, visualización del historial de transacciones, cálculos de cuotas y exportación de datos en PDF y Excel.

## **Tecnologías Utilizadas**
- **Backend:** ASP.NET Core 6, Entity Framework Core, MediatR, FluentValidation, AutoMapper.
- **Frontend:** ASP.NET MVC, Razor, jQuery, DataTables, TailwindCSS.
- **Base de Datos:** SQL Server con procedimientos almacenados (PL/SQL).
- **Tiempo Real:** SignalR para notificaciones en tiempo real.
- **Despliegue:** Azure App Services, Azure SQL Server.

## **Instalacion de la Base de Datos**
-  Requisitos Previos
   - Antes de ejecutar el script, asegúrate de tener instalado:
      *  SQL Server  y SQL Server Management Studio (SSMS).
   - Creación de la Base de Datos
      Abre SQL Server Management Studio (SSMS).
      Conéctate a tu servidor SQL.
      Crea una nueva base de datos llamada EstadoCuentaDB (o el nombre que prefieras).
      Ahora en File seleccionar Open y luego File y abre el archivo script SQL:
         Ubicado en [EstadoCuentaDBScript.sql](DBScript/EstadoCuentaDBScript.sql)
      Ejecuta el script presionando F5 o haciendo clic en Ejecutar.
      Verifica que se hayan creado las tablas y los procedimientos almacenados.
   - Conexión a la API
      Configura la cadena de conexión en appsettings.json de la API:

## Importar la colección de Postman

Para probar los endpoints de la API, importa el siguiente archivo en Postman:

[EstadoCuentaAPI.postman_collection.json](Postman/EstadoCuentaAPI.postman_collection.json)

**Pasos:**
1. Abre Postman y ve a **File > Import**.
2. Selecciona el archivo JSON exportado.
3. Asegúrate de configurar la variable `base_url` con la URL correcta de la API.
4. ¡Listo! Ya puedes probar los endpoints.

## Como probar el proyecto
Luego de descargar o clonar este repositorio sigue estos pasos:
   1. Abre la solución en Visual Studio
   2. Haz clic derecho en la solución (no en los proyectos).
   3. Después elige propiedades
   4. Ahora en la ventana de Proyecto de inicio escoja proyectos de inicio múltiple
   5. En acción escoja las opciones adecuadas 
   6. Clic en aceptar
   7. Presiona F5 en Visual Studio
   8. Visual Studio lanzará ambos proyectos simultáneamente
   9. La API se ejecutará en https://localhost:7264/ (o el puerto que tenga configurado).
   10. La aplicación Web se ejecutará en https://localhost:7025/ (o el puerto que tenga configurado).
   11. Abre la Web en el navegador y compruebe de que la API esté funcionando correctamente
   12. Prueba la API con Swagger en https://localhost:7264/swagger 
   13. Puedes usar la colección de Postman incluida en el repositorio

## **Arquitectura de la Solución**
La aplicación se divide en dos proyectos:
1. **EstadoCuenta.API** - API REST que gestiona las operaciones con la base de datos.
2. **EstadoCuenta.Web** - Aplicación web que consume la API y proporciona la interfaz de usuario.

## **Endpoints de la API**

### **Estado de Cuenta**
- `GET /api/EstadoCuenta/{tarjetaId}` - Obtiene el estado de cuenta.


### **Compras**
- `GET /api/Compras/{tarjetaId}` - Obtiene las compras del mes.
- `POST /api/Compras` - Registra una nueva compra.
  ```json
  {
    "tarjetaCreditoId": 1,
    "descripcion": "Compra en Supermercado",
    "monto": 150.25,
    "fecha": "2025-03-14T00:00:00"
  }
  ```

### **Pagos**
- `GET /api/Pagos/{tarjetaId}` - Obtiene los pagos del mes.
- `POST /api/Pagos` - Registra un nuevo pago.
  ```json
  {
    "tarjetaCreditoId": 1,
    "descripcion": "Pago a tarjeta",
    "monto": 200,
    "fecha": "2025-03-14T00:00:00"
  }
  ```

### **Tarjeta de Crédito**
- `GET /api/TarjetaCredito/{id}` - Obtiene los datos de una tarjeta.
- `POST /api/TarjetaCredito` - Registra una nueva tarjeta.
  ```json
  {
    "titular": "Juan Pérez",
    "numeroTarjeta": "1234567812345678",
    "saldoActual": 0,
    "limiteCredito": 2000
  }
  ```

### **Historial de Transacciones**
- `GET /api/Historiales/{tarjetaId}` - Obtiene el historial de transacciones del mes.

### **Exportación**
- `GET /api/Export/pdf/{tarjetaId}` - Exporta el estado de cuenta en PDF.
- `GET /api/Export/excel/{tarjetaId}` - Exporta las compras en Excel.

### **Pruebas**
- `GET /api/EstadoCuenta/prueba-error` - Prueba el manejo de errores.
- `GET /health` - Chequea el estado del servidor.

## **Funcionalidades de la Web**

### **Estado de Cuenta**
- Muestra el estado de cuenta con saldo, límite y cálculos de cuotas.
- Permite exportar en PDF y Excel.

### **Compras y Pagos**
- Formularios para registrar compras y pagos con validaciones.
- Tabla con historial de compras y pagos del mes.
- Notificaciones en tiempo real con SignalR.

### **Historial de Transacciones**
- Tabla con todas las transacciones del mes.
- Paginación y búsqueda con DataTables.
- Actualización en tiempo real con WebSockets.


## **Manejo de Errores y Validaciones**
Se ha implementado **FluentValidation** en la API. Ejemplo de error:
```json
{
  "errors": {
    "Monto": ["El monto debe ser mayor a 0"],
    "Fecha": ["La fecha no puede ser en el futuro"]
  }
}
```
https://estadocuenta-api-angdfwfeawhgfsee.canadacentral-01.azurewebsites.net/
## **Despliegue en Azure**
Se ha realizado el despliegue en Azure:
- **API:** [`https://estadocuenta-api-angdfwfeawhgfsee.canadacentral-01.azurewebsites.net/`](https://estadocuenta-api-angdfwfeawhgfsee.canadacentral-01.azurewebsites.net/)
- **Web:** [`https://estado-cuenta-web.azurewebsites.net`](https://estado-cuenta-web.azurewebsites.net)

## **Conclusión**
Este sistema proporciona una solución escalable para gestionar estados de cuenta de tarjetas de crédito, permitiendo registrar compras, pagos y consultar el historial.

