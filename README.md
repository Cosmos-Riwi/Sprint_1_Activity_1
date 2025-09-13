# Sprint_1_Activity_1
🎬 Intergalactic Cinema 🎟️

Un programa en C# que simula el sistema de precios del Cine Intergaláctico, donde el costo final de la boleta depende de múltiples factores: edad, tipo de película, día, hora, membresía, promociones, condición de estudiante y descuentos especiales.

Este proyecto aplica if anidados y reglas de negocio complejas para calcular el precio final de un ticket de cine.

🚀 Características principales

✔️ Calcula el precio final del boleto considerando:

Edad (niño, adolescente, adulto, senior).

Tipo de película (release, classic, 3d, marathon, special).

Día de la semana (monday → sunday).

Hora (morning, afternoon, night).

Membresía (none, silver, gold, platinum).

Promoción activa (true/false).

Condición de estudiante.

Compra en pareja (50% en segunda boleta).

✔️ Aplica reglas especiales como:

Miércoles de descuento global.

Restricciones de entrada por edad.

Bloqueo de descuentos viernes/sábado en la noche.

Descuentos acumulables por promociones o membresía.

Recargo en películas 3D (+10% después de descuentos).

✔️ Muestra un recibo detallado con:

Precio base.

Descuentos aplicados y motivos.

Precio final por persona o pareja.

📂 Estructura del proyecto
IntergalacticCinema/
│── Program.cs   # Código principal
│── README.md    # Documentación del proyecto

🖥️ Ejecución

Compila el proyecto en Rider, Visual Studio o usando la CLI de .NET:

dotnet run


El programa pedirá los siguientes datos por consola:

Edad

Tipo de película

Día de la semana

Hora

Membresía

Si hay promoción activa

Si es estudiante

Si compra en pareja

Precio base

Obtendrás un recibo con el desglose de descuentos y el precio final.

📋 Ejemplo de uso

Entrada de consola:

Enter age: 65
Enter movie type (release, classic, 3d, marathon, special): marathon
Enter day (monday, tuesday,wednesday,thursday,friday,saturday,sunday): sunday
Enter time (morning, afternoon, night): afternoon
Enter membership (none, silver, gold, platinum): gold
Is promo active? (true/false): true
Is student? (true/false): false
Couple ticket? (true/false): true
Enter base price: 20


Salida:

--- INTERGALACTIC CINEMA RECEIPT ---
Base price: $20.00
Age: 65, Type: marathon, Day: sunday, Time: afternoon, Membership: gold

Discount reasons:
 - Senior: 40% discount
 - Senior: Marathon fixed 50%.
 - Senior: Sunday promo 70%.
 - Couple: second ticket 50% off.

Total discount applied: 70%
Final ticket price: $6.00
Total to pay (couple): $9.00
------------------------------------

🎯 Objetivo académico

Este proyecto fue diseñado para:

Practicar estructuras condicionales complejas (if anidados).

Manejar entradas desde consola en C#.

Desarrollar lógica de negocio con múltiples excepciones.

Generar salidas claras y organizadas para el usuario.

🤝 Contribuciones

Si quieres mejorar el código (nuevas reglas, optimización o interfaz gráfica), puedes:

Hacer un fork del repositorio.

Crear una rama nueva.

Hacer un pull request con tus mejoras.

📜 Licencia

Este proyecto es de uso educativo y está disponible bajo la licencia MIT.
