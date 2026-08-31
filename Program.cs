using System;

// ================================================================
// CLASSE PRODUIT
// ================================================================
//
// Cette classe représente un produit dans un magasin.
//
// Chaque produit possède :
// - un nom
// - un prix
// - une quantité
//
// La classe protège ses données grâce à l'encapsulation.
// ================================================================

public class Produit
{
    // ============================================================
    // 1. CHAMPS PRIVÉS
    // ============================================================

    // Les champs sont privés afin d'empêcher leur modification
    // directe depuis l'extérieur de la classe.

    private string nom = string.Empty;
    private double prix;
    private int quantite;


    // ============================================================
    // 2. MEMBRE STATIC
    // ============================================================

    // Cette propriété appartient à la CLASSE Produit.
    // Il existe donc un seul compteur partagé par tous les objets.
    //
    // Le private set empêche le reste du programme
    // de modifier directement le compteur.
    public static int NbProduits { get; private set; }


    // ============================================================
    // 3. CONSTRUCTEUR
    // ============================================================

    // Le constructeur est appelé lorsqu'on écrit :
    //
    // new Produit(...)
    //
    // Son rôle est de créer un produit avec des valeurs valides.

    public Produit(string nom, double prix, int quantite)
    {
        // Nous utilisons les propriétés pour initialiser les champs.
        // Les validations présentes dans les propriétés seront
        // donc automatiquement exécutées.

        Nom = nom;
        Prix = prix;
        Quantite = quantite;

        // Un nouvel objet Produit vient réellement d'être créé.
        NbProduits++;
    }


    // ============================================================
    // 4. PROPRIÉTÉ NOM
    // ============================================================

    public string Nom
    {
        get
        {
            return nom;
        }

        set
        {
            // Le nom ne peut pas être vide.
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "Le nom du produit ne peut pas être vide."
                );
            }

            nom = value;
        }
    }


    // ============================================================
    // 5. PROPRIÉTÉ PRIX
    // ============================================================

    public double Prix
    {
        get
        {
            return prix;
        }

        set
        {
            // Un prix négatif n'est pas accepté.
            if (value < 0)
            {
                throw new ArgumentException(
                    "Le prix ne peut pas être négatif."
                );
            }

            prix = value;
        }
    }


    // ============================================================
    // 6. PROPRIÉTÉ QUANTITÉ
    // ============================================================

    public int Quantite
    {
        get
        {
            return quantite;
        }

        set
        {
            // Une quantité négative n'est pas acceptée.
            if (value < 0)
            {
                throw new ArgumentException(
                    "La quantité ne peut pas être négative."
                );
            }

            quantite = value;
        }
    }


    // ============================================================
    // 7. MÉTHODE VALEURSTOCK
    // ============================================================

    // Cette méthode calcule la valeur totale du stock :
    //
    // prix × quantité

    public double ValeurStock()
    {
        return Prix * Quantite;
    }
}


// =================================================================
// PROGRAMME PRINCIPAL
// =================================================================

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=================================");
        Console.WriteLine("       LABO - CLASSE PRODUIT");
        Console.WriteLine("=================================");


        // =========================================================
        // TEST 1 : CRÉER UN PREMIER OBJET
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("--- Test 1 : création d'un produit ---");

        Produit p1 = new Produit(
            "Clavier",
            49.99,
            10
        );

        Console.WriteLine($"Nom      : {p1.Nom}");
        Console.WriteLine($"Prix     : {p1.Prix:F2} $");
        Console.WriteLine($"Quantité : {p1.Quantite}");
        Console.WriteLine(
            $"Valeur du stock : {p1.ValeurStock():F2} $"
        );


        // =========================================================
        // TEST 2 : STATIC
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("--- Test 2 : membre static ---");

        // Jusqu'ici, un seul objet a été créé.
        Console.WriteLine(
            $"Nombre de produits créés : {Produit.NbProduits}"
        );

        // Résultat attendu :
        // 1


        // =========================================================
        // TEST 3 : COPIE DE RÉFÉRENCE
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("--- Test 3 : copie de référence ---");

        // IMPORTANT :
        //
        // Cette ligne NE crée PAS un nouvel objet.
        //
        // Elle copie simplement la référence contenue dans p1.
        Produit p2 = p1;


        // Maintenant :
        //
        // p1 ───┐
        //       ├──> même objet Produit
        // p2 ───┘


        // Modification avec p2.
        p2.Quantite = 3;


        Console.WriteLine(
            $"Quantité avec p1 : {p1.Quantite}"
        );

        Console.WriteLine(
            $"Quantité avec p2 : {p2.Quantite}"
        );


        // Résultat attendu :
        //
        // p1.Quantite = 3
        // p2.Quantite = 3
        //
        // Pourquoi ?
        //
        // Parce que p1 et p2 pointent vers le même objet.


        // =========================================================
        // TEST 4 : VALEUR DU STOCK APRÈS MODIFICATION
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("--- Test 4 : valeur du stock ---");

        Console.WriteLine(
            $"Prix : {p1.Prix:F2} $"
        );

        Console.WriteLine(
            $"Quantité : {p1.Quantite}"
        );

        Console.WriteLine(
            $"Valeur du stock : {p1.ValeurStock():F2} $"
        );

        // 49.99 × 3 = 149.97


        // =========================================================
        // TEST 5 : VÉRIFIER NbProduits
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("--- Test 5 : nombre de produits ---");

        Console.WriteLine(
            $"NbProduits = {Produit.NbProduits}"
        );

        // Résultat attendu :
        // 1
        //
        // Pourquoi ?
        //
        // Produit p2 = p1;
        //
        // n'a PAS créé un nouveau Produit.
        // Aucun constructeur n'a été appelé.


        // =========================================================
        // TEST 6 : CRÉER UN VRAI DEUXIÈME OBJET
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("--- Test 6 : nouvel objet ---");

        Produit p3 = new Produit(
            "Souris",
            29.99,
            5
        );

        Console.WriteLine($"Nom      : {p3.Nom}");
        Console.WriteLine($"Prix     : {p3.Prix:F2} $");
        Console.WriteLine($"Quantité : {p3.Quantite}");

        Console.WriteLine(
            $"Valeur du stock : {p3.ValeurStock():F2} $"
        );


        Console.WriteLine(
            $"Nombre de produits créés : {Produit.NbProduits}"
        );

        // Résultat attendu :
        // 2
        //
        // Cette fois, new Produit(...) a appelé le constructeur.


        // =========================================================
        // TEST 7 : DEUX OBJETS INDÉPENDANTS
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("--- Test 7 : objets indépendants ---");

        // p1 et p3 sont deux objets différents.

        p3.Quantite = 20;

        Console.WriteLine(
            $"Quantité de p1 : {p1.Quantite}"
        );

        Console.WriteLine(
            $"Quantité de p3 : {p3.Quantite}"
        );

        // Modifier p3 ne modifie pas p1.


        // =========================================================
        // TEST 8 : VALIDATION DU PRIX
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("--- Test 8 : validation du prix ---");

        try
        {
            Produit produitInvalide = new Produit(
                "Écran",
                -200,
                4
            );
        }
        catch (ArgumentException erreur)
        {
            Console.WriteLine(
                $"Erreur : {erreur.Message}"
            );
        }


        // =========================================================
        // TEST 9 : VALIDATION DE LA QUANTITÉ
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("--- Test 9 : validation de la quantité ---");

        try
        {
            p1.Quantite = -5;
        }
        catch (ArgumentException erreur)
        {
            Console.WriteLine(
                $"Erreur : {erreur.Message}"
            );
        }


        // =========================================================
        // TEST 10 : VALIDATION DU NOM
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("--- Test 10 : validation du nom ---");

        try
        {
            Produit produitSansNom = new Produit(
                "",
                10,
                2
            );
        }
        catch (ArgumentException erreur)
        {
            Console.WriteLine(
                $"Erreur : {erreur.Message}"
            );
        }


        // =========================================================
        // FIN
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("=================================");
        Console.WriteLine("          FIN DU LABO");
        Console.WriteLine("=================================");
    }
}