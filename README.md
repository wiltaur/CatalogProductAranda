# CatalogProductAranda

Este es una API Restful desarrollada para prueba en Aranda Software.

La BD que se usa es local con Sql Server "SQLEXPRESS" y se trabajó con el usuario sa, dándole una contraseña inicial con la cual se conectó al ORM Entity Framework.

Los scripts de la Base de Datos están en la carpeta: "Scripts_BD".

Para tener en cuenta:
- Las imágenes deben llegar a la API en formato BASE64, así mismo en la consulta ésta es devuelta en el mismo formato. En la Base de datos guarda un Array de bytes.
- El método que lista los productos fue desarrollado pensando en una vista con una tabla con un campo de búsqueda, las columnas indicadas con opción de click de ordenamiento y también con una paginación.
- Se desarrollaron algunas pruebas unitarias con XUnit al Controller y al servicio.
