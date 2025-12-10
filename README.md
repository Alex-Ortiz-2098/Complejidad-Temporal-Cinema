En un salón de fiestas se tienen registrados los eventos contratados y el conjunto de empleados que
trabaja de forma fija en los eventos. De cada evento se almacena: nombre y dni del cliente, fecha y
hora del evento, tipo de evento (cumpleaños de 15, casamientos, bautismos, etc.. ), encargado, lista
de servicios contratados, costo total (se calcula en base al precio de los servicios contratados y la
cantidad) y monto de la seña. De cada servicio ofrecido se almacena nombre del servicio (catering,
bebida, mozos, DJ, inflables, cama elástica, etc), descripción (detalle de lo que incluye el servicio),
cantidad solicitada, costo unitario del servicio. De cada empleado se registra su nombre y apellido,
dni, nro de legajo, sueldo y tarea que desempeña. El encargado de un evento es un empleado que
organiza y controla el desarrollo del evento y cobra un plus sobre el sueldo.
Se deberá desarrollar una aplicación, utilizando las clases que considere necesarias, utilizando
herencia cuando corresponda. La aplicación debe proveer, mediante un menú, las siguientes
funcionalidades:
##
  a- Agregar un servicio
  ##
  b- Eliminar un servicio.
  ##
  c- Dar de alta un empleado/encargado
  d- Dar de baja un empleado/encargado
  e- Reservar el salón para un evento. El cliente puede incluir en su pedido un solo servicio o
  varios. El salón toma una sola reserva para la misma fecha. En caso de que ya tenga una
  reserva previa se levanta una excepción indicando lo ocurrido. Al confirmar la reserva se le
  asigna un encargado al evento.
  f- Cancelar un evento. En caso que el cliente solicite la cancelación con más de un mes de
  anticipación a la fecha del servicio, no se le reintegra la seña. En otro caso, el cliente debe
  abonar el servicio completo.
  g- Submenú de impresión: listado de eventos, de clientes, de empleados, listado de eventos de
  un mes determinado
