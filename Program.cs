using System;

class CineIntergalactico
{
    static void Main(string[] args)
    {
        // Entradas solicitadas al usuario
        Console.Write("Ingrese la edad: ");
        int edad = int.Parse(Console.ReadLine());

        Console.Write("Ingrese el tipo de película (estreno, clasico, 3D, maraton, funcion_especial): ");
        string tipo_pelicula = Console.ReadLine().ToLower();

        Console.Write("Ingrese el día (lunes, martes, miercoles, jueves, viernes, sabado o domingo): ");
        string dia = Console.ReadLine().ToLower();

        Console.Write("Ingrese la hora (mañana, tarde, noche): ");
        string hora = Console.ReadLine().ToLower();

        Console.Write("Ingrese la membresía (ninguna, silver, gold, platino): ");
        string membresia = Console.ReadLine().ToLower();

        Console.Write("¿Hay promoción activa? (true/false): ");
        bool promo_activa = bool.Parse(Console.ReadLine());

        Console.Write("¿Es estudiante? (true/false): ");
        bool estudiante = bool.Parse(Console.ReadLine());

        Console.Write("¿Viene en pareja? (true/false): ");
        bool pareja = bool.Parse(Console.ReadLine());

        Console.Write("Ingrese el precio base de la boleta: ");
        double precio_base = double.Parse(Console.ReadLine());

        // Variables de trabajo
        double precio_final = precio_base;
        string descuentos = "";
        
        // 1. EDAD
        if (edad < 12)
        {
            if (tipo_pelicula == "clasico" && (dia == "lunes" || dia == "miercoles"))
            {
                precio_final = 0;
                descuentos += "Niños gratis en clásicos tanto lunes como miércoles. ";
            }
            else if (tipo_pelicula == "3d" && (hora == "mañana" || hora == "tarde"))
            {
                precio_final *= 0.30;
                descuentos += "Niño paga 30% en 3D antes de las 6pm. ";
            }
            else if (tipo_pelicula == "funcion_especial")
            {
                Console.WriteLine("los niños no pueden entrar a funciones especiales.");
                return;
            }
        }
        else if (edad >= 12 && edad <= 17)
        {
            if (tipo_pelicula == "estreno")
            {
                descuentos += "Adolescente paga completo en estrenos. ";
            }
            else if (tipo_pelicula == "clasico" && dia == "miercoles")
            {
                precio_final *= 0.50;
                descuentos += "Adolescente 50% en clásico los miércoles. ";
            }
            else if (tipo_pelicula == "3d" && membresia == "silver" && dia != "domingo")
            {
                precio_final *= 0.80;
                descuentos += "Adolescente Silver 20% en 3D, menos el domingo. ";
            }
        }
        else if (edad >= 18 && edad <= 59)
        {
            if (tipo_pelicula == "funcion_especial")
            {
                descuentos += "Adulto paga completo en función especial. ";
            }
            else if (tipo_pelicula == "estreno")
            {
                descuentos += "Adulto paga completo en estreno. ";
            }

            if (membresia == "gold")
            {
                if (tipo_pelicula == "clasico" && !(dia == "viernes" || dia == "sabado" && hora == "noche"))
                {
                    precio_final *= 0.75;
                    descuentos += "Membresía Gold 25% en clásico. ";
                }
                else if (tipo_pelicula == "3d" && dia != "domingo")
                {
                    precio_final *= 0.85;
                    descuentos += "Membresía Gold 15% en 3D (no domingo). ";
                }
            }
            else if (membresia == "platino")
            {
                if (!(tipo_pelicula == "estreno" && dia == "sabado" && hora == "noche"))
                {
                    precio_final *= 0.65;
                    descuentos += "Membresía Platino 35% en todo (salvo estreno sábado noche). ";
                }
            }
        }
        else if (edad >= 60)
        {
            if (tipo_pelicula == "maraton")
            {
                precio_final *= 0.50;
                descuentos += "Mayor paga 50% fijo en maratón. ";
            }
            else if (dia == "domingo" && promo_activa)
            {
                precio_final *= 0.30;
                descuentos += "Mayor 70% descuento en domingo con promo activa. ";
            }
            else
            {
                precio_final *= 0.60;
                descuentos += "Mayor siempre 40% de descuento. ";
            }
        }

        // 2. DÍA
        if (dia == "miercoles" && tipo_pelicula != "funcion_especial")
        {
            precio_final *= 0.80;
            descuentos += "Descuento global del miércoles (20%). ";
        }
        else if ((dia == "viernes" || dia == "sabado") && hora == "noche")
        {
            descuentos += "Viernes/sábado noche sin descuentos de membresía. ";
        }

        // 3. TIPO DE PELÍCULA
        if (tipo_pelicula == "estreno")
        {
            if (estudiante)
            {
                precio_final *= 0.85;
                descuentos += "Estudiante 15% en estreno. ";
            }
            else if (membresia == "platino" && !(dia == "sabado" && hora == "noche"))
            {
                precio_final *= 0.65;
                descuentos += "Platino 35% en estreno (no sábado noche). ";
            }
        }
        else if (tipo_pelicula == "3d")
        {
            precio_final *= 1.10;
            descuentos += "Recargo 10% por 3D. ";
        }
        else if (tipo_pelicula == "maraton" && edad < 60)
        {
            precio_final *= 0.80;
            descuentos += "Maratón 20% descuento general. ";
        }
        else if (tipo_pelicula == "funcion_especial" && edad < 18)
        {
            Console.WriteLine("No puedes entrar a funciones especiales si no eres adulto/mayor.");
            return;
        }

        // 4. EXTRAS
        if (estudiante && (dia == "lunes" || dia == "miercoles"))
        {
            precio_final *= 0.90;
            descuentos += "Estudiante lunes/miércoles 10% extra. ";
        }
        if (pareja && dia != "domingo")
        {
            descuentos += "Pareja: el segundo boleto al 50%. ";
        }
        if (promo_activa)
        {
            if (dia == "domingo" && tipo_pelicula != "funcion_especial")
            {
                precio_final *= 0.90;
                descuentos += "Promo domingo 10% extra. ";
            }
            else if (dia != "domingo" && (membresia == "silver" || membresia == "gold" || membresia == "platino"))
            {
                precio_final *= 0.95;
                descuentos += "Promo activa +5% para membresía Silver o superior. ";
            }
        }

        Console.WriteLine($"\nPrecio base: {precio_base}");
        Console.WriteLine($"Descuentos aplicados: {descuentos}");
        Console.WriteLine($"Precio final: {precio_final}");
    }
}
