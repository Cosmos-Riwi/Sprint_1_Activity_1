class CineIntergalactico 
{
    static void Main()
    {
        /*====================
        Variables de entrada
        ====================*/
        int edad;
        string tipo_pelicula, dia, hora, membresia;
        bool promo_activa, estudiante, pareja; 
        
        /*================
        Entrada de datos
        ================*/
        Console.WriteLine("🎬 Bienvenidos al Cine Intergalactico ");
        Console.WriteLine("---------------------------------------");
        
        Console.Write("Ingrese su edad: ");
        edad = int.Parse(Console.ReadLine());
        
        Console.Write("Ingrese el tipo de pelicula (estreno, clasico, 3D, maraton, funcion_especial): ");
        tipo_pelicula = Console.ReadLine().ToLower();
        
        Console.Write("Ingrese el dia (lunes, martes, miercoles, jueves, viernes, sabado, domingo): ");
        dia = Console.ReadLine().ToLower();
        
        Console.Write("Ingrese la hora (mañana, tarde, noche): ");
        hora = Console.ReadLine().ToLower();
        
        Console.Write("Ingrese el tipo de membresia (ninguna, silver, gold, platino): ");
        membresia = Console.ReadLine().ToLower();
        
        Console.Write("¿Tiene una promo activa? (true/false): ");
        promo_activa = bool.Parse(Console.ReadLine());
        
        Console.Write("¿Eres estudiante? (true/false): ");
        estudiante = bool.Parse(Console.ReadLine());
        
        Console.Write("¿Vienes con pareja? (true/false): ");
        pareja = bool.Parse(Console.ReadLine());
        
        /*=================
        Entrada de datos
        =================*/
        double precio_base = 10000;
        double precio_final = precio_base;
        string descuentos = "";
        
        /*============
        Validaciones
        ============*/
         
        //Validacion de edad
        if (edad < 12)
        {
            if (tipo_pelicula == "clasico" && (dia == "lunes" || dia == "miercoles"))
            {
                descuentos += "Niño: Gratis en clasicos lunes/miercoles. ";
                precio_final = 0;
            }
            else if (tipo_pelicula == "3D" && hora != "noche")
            {
                descuentos += "Niño: 70% de descuento en 3D antes de las 6pm. ";
                precio_final *= 0.3;
            }
            else if (tipo_pelicula == "funcion_especial")
            {
                Console.WriteLine("⚠ Lo sentimos, los niños no pueden entrar a funciones especiales.");
                return;
            }
        }
        else if (edad >= 12 && edad <= 17)
        {
            if (tipo_pelicula == "clasico" && dia == "miercoles")
            {
                descuentos += "Adolescente: 50% en clasicos los miercoles. ";
                precio_final *= 0.5;
            }
            if (tipo_pelicula == "3d" && membresia == "silver" && dia != "domingo")
            {
                descuentos += "Adolescente + Silver: 20% en 3D. ";
                precio_final *= 0.8;
            }
        }
        else if (edad >= 18 && edad <= 59)
        {
            if (membresia == "gold" && tipo_pelicula == "clasico" && !(dia == "viernes" || dia == "sabado"))
            {
                descuentos += "Adulto + Gold: 25% en clasicos. ";
                precio_final *= 0.75;
            }
            if (membresia == "gold" && tipo_pelicula == "3D" && dia != "domingo")
            {
                descuentos += "Adulto + Gold: 15% en 3D. ";
                precio_final *= 0.85;
            }
            if (membresia == "platino")
            {
                if (!(tipo_pelicula == "estreno" && dia == "sabado" && hora == "noche"))
                {
                    descuentos += "Adulto + Platino: 35% en todo. ";
                    precio_final *= 0.65;
                }
            }
        }
        else if (edad >= 60)
        {
            if (tipo_pelicula == "maraton")
            {
                descuentos += "Senior: 50% fijo en maraton. ";
                precio_final *= 0.5;
            }
            else if (dia == "domingo" && promo_activa)
            {
                descuentos += "Senior: 70% en domingo con promo. ";
                precio_final *= 0.3;
            }
            else
            {
                descuentos += "Senior: 40% en todo. ";
                precio_final *= 0.6;
            }
        }
        
        //Validacion dia de la semana
        if (dia == "miercoles" && tipo_pelicula != "funcion_especial")
        {
            descuentos += "Miercoles de descuento: 80%. ";
            precio_final *= 0.2;
        }
        else if ((dia == "viernes" || dia == "sabado") && hora == "noche")
        {
            descuentos += "viernes/sabado noche: bloquea descuentos por membresia. ";
        }
        
        //Validacion tipo de pelicula
        if (tipo_pelicula == "3D")
        {
            descuentos += "Recargo 3D: +10%. ";
            precio_final *= 1.1;
        }
        else if (tipo_pelicula == "maraton" && edad < 60)
        {
            descuentos += "Maraton: 20% descuento general. ";
            precio_final *= 0.8;
        }
        else if (tipo_pelicula == "funcion_especial" && edad < 18)
        {
            Console.WriteLine("⚠ Los menores de edad no pueden entrar a funciones especiales.");
            return;
        }
        
        //Validacion promos
        if (estudiante && (dia == "lunes" || dia == "miercoles"))
        {
            descuentos += "Estudiante: 10% extra. ";
            precio_final *= 0.9;
        }
        
        if (pareja && dia != "domingo")
        {
            descuentos += "Pareja: 2x1 con 50% en la segunda entrada. ";
            precio_final = precio_final + (precio_final * 0.5); // suma el segundo ticket con 50%
        }
        
        if (promo_activa)
        {
            if (dia == "domingo" && tipo_pelicula != "funcion_especial")
            {
                descuentos += "Promo activa domingo: -10%. ";
                precio_final *= 0.9;
            }
            else if (membresia != "ninguna")
            {
                descuentos += "Promo activa + membresia: -5%. ";
                precio_final *= 0.95;
            }
        }
        
        
        /*=================
        🎟 Resultado final
        =================*/
        Console.WriteLine("\n========== BOLETA INTERGALÁCTICA ==========");
        Console.WriteLine($"Precio base: 10,000 pesos");
        Console.WriteLine($"Descuentos aplicados: {descuentos}");
        Console.WriteLine($"💰 Precio final a pagar: {precio_final} pesos");
        Console.WriteLine("===========================================");
       
    }
}
