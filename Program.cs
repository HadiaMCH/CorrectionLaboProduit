using System;

// ================================================================
// CLASSE MÈRE : Produit
// ================================================================
//
// Produit contient tout ce qui est COMMUN
// à tous les produits du magasin.
//
// Les sous-classes vont hériter de cette classe.
// ================================================================

public class Produit
{
    // ------------------------------------------------------------
    // CHAMPS PRIVÉS
    // ------------------------------------------------------------

    private string nom = string.Empty;
    private double prix;
    private int quantite;


    // ------------------------------------------------------------
    // MEMBRE STATIC
    // ------------------------------------------------------------
    //
    // Ce compteur est partagé par Produit
    // ET toutes ses sous-classes.

    public static int NbProduits { get; private set; }


    // ------------------------------------------------------------
    // CONSTRUCTEUR DE Produit
    // ------------------------------------------------------------

    public Produit(string nom, double prix, int quantite)
    {
        // On passe par les propriétés
        // afin d'utiliser leurs validations.
        Nom = nom;
        Prix = prix;
        Quantite = quantite;

        NbProduits++;
    }


    // ------------------------------------------------------------
    // PROPRIÉTÉ Nom
    // ------------------------------------------------------------

    public string Nom
    {
        get
        {
            return nom;
        }

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "Le nom du produit ne peut pas être vide."
                );
            }

            nom = value;
        }
    }


    // ------------------------------------------------------------
    // PROPRIÉTÉ Prix
    // ------------------------------------------------------------

    public double Prix
    {
        get
        {
            return prix;
        }

        set
        {
            if (value < 0)
            {
                throw new ArgumentException(
                    "Le prix ne peut pas être négatif."
                );
            }

            prix = value;
        }
    }


    // ------------------------------------------------------------
    // PROPRIÉTÉ Quantite
    // ------------------------------------------------------------

    public int Quantite
    {
        get
        {
            return quantite;
        }

        set
        {
            if (value < 0)
            {
                throw new ArgumentException(
                    "La quantité ne peut pas être négative."
                );
            }

            quantite = value;
        }
    }


    // ------------------------------------------------------------
    // MÉTHODE COMMUNE
    // ------------------------------------------------------------

    public double ValeurStock()
    {
        return Prix * Quantite;
    }


    // ------------------------------------------------------------
    // MÉTHODE VIRTUAL
    // ------------------------------------------------------------
    //
    // virtual signifie :
    //
    // "Produit possède une version de Afficher(),
    // mais une sous-classe pourra la redéfinir."

    public virtual void Afficher()
    {
        Console.WriteLine($"Nom             : {Nom}");
        Console.WriteLine($"Prix            : {Prix:F2} $");
        Console.WriteLine($"Quantité        : {Quantite}");
        Console.WriteLine(
            $"Valeur du stock : {ValeurStock():F2} $"
        );
    }
}


// ================================================================
// SOUS-CLASSE : ProduitElectronique
// ================================================================
//
// Relation :
//
// ProduitElectronique EST UN Produit.
//
// ProduitElectronique hérite donc de Produit.
// ================================================================

public class ProduitElectronique : Produit
{
    // Information spécifique aux produits électroniques.
    private int garantieMois;


    // ------------------------------------------------------------
    // CONSTRUCTEUR
    // ------------------------------------------------------------

    public ProduitElectronique(
        string nom,
        double prix,
        int quantite,
        int garantieMois
    )
        : base(nom, prix, quantite)
    {
        // base(...) appelle le constructeur de Produit.

        // Ensuite, ProduitElectronique initialise
        // sa propre information.
        this.garantieMois = garantieMois;
    }


    public int GarantieMois
    {
        get
        {
            return garantieMois;
        }
    }


    // ------------------------------------------------------------
    // OVERRIDE
    // ------------------------------------------------------------
    //
    // override remplace la version générale
    // de Afficher() pour ProduitElectronique.

    public override void Afficher()
    {
        // Appeler d'abord la version commune.
        base.Afficher();

        // Puis ajouter ce qui est spécifique.
        Console.WriteLine("Type            : Électronique");
        Console.WriteLine(
            $"Garantie        : {garantieMois} mois"
        );
    }


    // Méthode uniquement disponible
    // pour ProduitElectronique.
    public void AfficherGarantie()
    {
        Console.WriteLine(
            $"Garantie de {Nom} : {garantieMois} mois."
        );
    }
}


// ================================================================
// SOUS-CLASSE : ProduitAlimentaire
// ================================================================
//
// ProduitAlimentaire EST UN Produit.
//
// Il ajoute une date d'expiration.
// ================================================================

public class ProduitAlimentaire : Produit
{
    private DateTime expiration;


    public ProduitAlimentaire(
        string nom,
        double prix,
        int quantite,
        DateTime expiration
    )
        : base(nom, prix, quantite)
    {
        this.expiration = expiration;
    }


    public DateTime Expiration
    {
        get
        {
            return expiration;
        }
    }


    public override void Afficher()
    {
        // Réutiliser l'affichage commun.
        base.Afficher();

        // Ajouter l'information spécifique.
        Console.WriteLine("Type            : Alimentaire");

        Console.WriteLine(
            $"Expiration      : {expiration:yyyy-MM-dd}"
        );
    }


    public bool EstExpire(DateTime date)
    {
        return expiration.Date < date.Date;
    }
}


// ================================================================
// SOUS-CLASSE : ProduitLivre
// ================================================================
//
// Cette troisième sous-classe sert à démontrer
// un avantage important du polymorphisme.
//
// On peut ajouter un nouveau type de Produit
// sans réécrire la boucle foreach.
// ================================================================

public class ProduitLivre : Produit
{
    private string auteur;


    public ProduitLivre(
        string nom,
        double prix,
        int quantite,
        string auteur
    )
        : base(nom, prix, quantite)
    {
        this.auteur = auteur;
    }


    public string Auteur
    {
        get
        {
            return auteur;
        }
    }


    public override void Afficher()
    {
        base.Afficher();

        Console.WriteLine("Type            : Livre");
        Console.WriteLine($"Auteur          : {auteur}");
    }
}


// ================================================================
// PROGRAMME PRINCIPAL
// ================================================================

public class Program
{
    public static void Main(string[] args)
    {
        // ========================================================
        // TEST 1 - CRÉATION D'UN PRODUIT
        // ========================================================

        Titre("TEST 1 - Produit général");

        Produit p1 = new Produit(
            "Cahier",
            4.99,
            10
        );

        p1.Afficher();

        Console.WriteLine(
            $"Nombre de produits : {Produit.NbProduits}"
        );


        // ========================================================
        // TEST 2 - CRÉATION DES SOUS-CLASSES
        // ========================================================

        Titre("TEST 2 - Sous-classes");

        ProduitElectronique electronique =
            new ProduitElectronique(
                "Écouteurs",
                79.99,
                8,
                24
            );

        ProduitAlimentaire alimentaire =
            new ProduitAlimentaire(
                "Yogourt",
                3.99,
                20,
                new DateTime(2026, 9, 15)
            );

        ProduitLivre livre =
            new ProduitLivre(
                "Clean Code",
                49.95,
                4,
                "Robert C. Martin"
            );


        electronique.Afficher();

        Console.WriteLine();

        alimentaire.Afficher();

        Console.WriteLine();

        livre.Afficher();


        // ========================================================
        // TEST 3 - STATIC ET HÉRITAGE
        // ========================================================

        Titre("TEST 3 - static");

        Console.WriteLine(
            $"Nombre total de produits créés : {Produit.NbProduits}"
        );

        /*
         * Résultat attendu :
         *
         * 4
         *
         * 1 Produit
         * 1 ProduitElectronique
         * 1 ProduitAlimentaire
         * 1 ProduitLivre
         *
         * Le compteur appartient à Produit
         * mais il est partagé avec toutes les sous-classes.
         */


        // ========================================================
        // TEST 4 - RÉFÉRENCE PARENT / OBJET ENFANT
        // ========================================================

        Titre("TEST 4 - Type déclaré et type réel");


        // TYPE DÉCLARÉ :
        // Produit
        //
        // TYPE RÉEL :
        // ProduitElectronique

        Produit produit = electronique;


        Console.WriteLine(
            "Type déclaré : Produit"
        );

        Console.WriteLine(
            $"Type réel    : {produit.GetType().Name}"
        );


        // IMPORTANT :
        //
        // La variable est Produit,
        // mais l'objet réel est ProduitElectronique.

        produit.Afficher();


        /*
         * Quelle méthode Afficher() est appelée ?
         *
         * ProduitElectronique.Afficher()
         *
         * Pourquoi ?
         *
         * Parce que :
         *
         * Produit.Afficher()
         * est virtual
         *
         * et
         *
         * ProduitElectronique.Afficher()
         * est override.
         *
         * C# regarde donc le TYPE RÉEL de l'objet.
         */


        // ========================================================
        // TEST 5 - POLYMORPHISME
        // ========================================================

        Titre("TEST 5 - Polymorphisme");


        // Le tableau est de type Produit[].
        //
        // Pourtant, les objets à l'intérieur
        // sont de types différents.

        Produit[] inventaire =
        {
            electronique,
            alimentaire,
            livre
        };


        foreach (Produit item in inventaire)
        {
            Console.WriteLine();

            Console.WriteLine("----------------------------");

            // Toujours le même appel.
            item.Afficher();
        }


        /*
         * C'est ici qu'on voit le polymorphisme.
         *
         * La boucle écrit toujours :
         *
         * item.Afficher();
         *
         * Pourtant :
         *
         * ProduitElectronique
         * -> appelle Afficher électronique
         *
         * ProduitAlimentaire
         * -> appelle Afficher alimentaire
         *
         * ProduitLivre
         * -> appelle Afficher livre
         */


        // ========================================================
        // TEST 6 - CONTRE-CAS
        // ========================================================

        Titre("TEST 6 - Pourquoi éviter les if");


        Console.WriteLine(
            "On pourrait tester chaque type avec des if..."
        );

        Console.WriteLine();

        /*
         * MAUVAISE APPROCHE POUR Afficher() :
         *
         * foreach (Produit item in inventaire)
         * {
         *     if (item is ProduitElectronique)
         *     {
         *         ...
         *     }
         *     else if (item is ProduitAlimentaire)
         *     {
         *         ...
         *     }
         *     else if (item is ProduitLivre)
         *     {
         *         ...
         *     }
         * }
         *
         * Pourquoi c'est mauvais ici ?
         *
         * Parce que chaque fois qu'on ajoute une nouvelle sous-classe,
         * il faut modifier cette boucle.
         *
         * Le polymorphisme permet plutôt :
         *
         * item.Afficher();
         */


        foreach (Produit item in inventaire)
        {
            item.Afficher();
            Console.WriteLine();
        }


        // ========================================================
        // TEST 7 - is POUR UN BESOIN SPÉCIAL
        // ========================================================

        Titre("TEST 7 - is pour un cas spécial");


        foreach (Produit item in inventaire)
        {
            /*
             * Afficher() ne nécessite PAS de is.
             *
             * Mais imaginons que nous voulions appeler
             * AfficherGarantie().
             *
             * Cette méthode existe seulement
             * dans ProduitElectronique.
             */

            if (item is ProduitElectronique e)
            {
                e.AfficherGarantie();
            }
        }


        // ========================================================
        // TEST 8 - as ET CONVERSION
        // ========================================================

        Titre("TEST 8 - as");


        Produit produitGeneral = alimentaire;


        // On essaie de convertir un ProduitAlimentaire
        // en ProduitElectronique.
        //
        // Cette conversion est impossible.

        ProduitElectronique? e2 =
            produitGeneral as ProduitElectronique;


        if (e2 == null)
        {
            Console.WriteLine(
                "Conversion impossible."
            );

            Console.WriteLine(
                "L'objet réel est un ProduitAlimentaire."
            );
        }


        /*
         * Tous les ProduitElectronique sont des Produit.
         *
         * MAIS
         *
         * tous les Produit ne sont pas
         * des ProduitElectronique.
         */


        // ========================================================
        // TEST 9 - COPIE DE RÉFÉRENCE
        // ========================================================

        Titre("TEST 9 - Copie de référence");


        Produit reference1 = electronique;

        Produit reference2 = reference1;


        // Les deux variables pointent vers le même objet.

        reference2.Quantite = 2;


        Console.WriteLine(
            $"Quantité avec reference1 : {reference1.Quantite}"
        );

        Console.WriteLine(
            $"Quantité avec reference2 : {reference2.Quantite}"
        );

        Console.WriteLine(
            $"Quantité avec electronique : {electronique.Quantite}"
        );


        /*
         * Résultat :
         *
         * 2
         * 2
         * 2
         *
         * Pourquoi ?
         *
         * reference2 = reference1
         *
         * ne copie pas l'objet.
         *
         * Cela copie la référence.
         */


        // ========================================================
        // TEST 10 - VALIDATION HÉRITÉE
        // ========================================================

        Titre("TEST 10 - Validation héritée");


        try
        {
            ProduitElectronique invalide =
                new ProduitElectronique(
                    "",
                    100,
                    5,
                    12
                );
        }
        catch (ArgumentException erreur)
        {
            Console.WriteLine(
                $"Erreur détectée : {erreur.Message}"
            );
        }


        /*
         * ProduitElectronique appelle :
         *
         * base(nom, prix, quantite)
         *
         * Donc les validations de Produit
         * sont aussi utilisées lorsque nous créons
         * un ProduitElectronique.
         */


        // ========================================================
        // TEST 11 - AJOUTER UNE NOUVELLE SOUS-CLASSE
        // ========================================================

        Titre("TEST 11 - Extensibilité");


        Console.WriteLine(
            "Nous avons ajouté ProduitLivre."
        );

        Console.WriteLine(
            "Pourtant, la boucle polymorphe n'a pas changé."
        );


        Produit[] inventaire2 =
        {
            new ProduitElectronique(
                "Télévision",
                899.99,
                3,
                36
            ),

            new ProduitAlimentaire(
                "Lait",
                4.49,
                15,
                new DateTime(2026, 9, 20)
            ),

            new ProduitLivre(
                "C# pour débutants",
                39.99,
                6,
                "Marie Tremblay"
            )
        };


        foreach (Produit item in inventaire2)
        {
            Console.WriteLine();

            item.Afficher();
        }


        // ========================================================
        // FIN
        // ========================================================

        Titre("FIN DU LABO");
    }


    // ============================================================
    // MÉTHODE UTILITAIRE
    // ============================================================
    //
    // Elle sert seulement à rendre l'affichage console plus clair.

    private static void Titre(string texte)
    {
        Console.WriteLine();
        Console.WriteLine(
            "================================================="
        );

        Console.WriteLine(texte);

        Console.WriteLine(
            "================================================="
        );
    }
}