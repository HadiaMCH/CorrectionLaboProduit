using System;
using System.Collections.Generic;

// ============================================================================
// CLASSE MÈRE : Produit
// ============================================================================
//
// Cette classe contient les informations communes à tous les produits.
//
// Les notions du bloc 3 restent présentes :
// - héritage
// - virtual / override
// - polymorphisme
//
// Le bloc 4 ajoutera les collections autour de ces objets.
// ============================================================================

public class Produit
{
    private string code = string.Empty;
    private string nom = string.Empty;
    private double prix;
    private int quantite;

    // Compteur partagé par Produit et toutes ses sous-classes.
    public static int NbProduits { get; private set; }


    // ------------------------------------------------------------------------
    // CONSTRUCTEUR
    // ------------------------------------------------------------------------

    public Produit(
        string code,
        string nom,
        double prix,
        int quantite
    )
    {
        Code = code;
        Nom = nom;
        Prix = prix;
        Quantite = quantite;

        NbProduits++;
    }


    // ------------------------------------------------------------------------
    // PROPRIÉTÉ Code
    // ------------------------------------------------------------------------
    //
    // Le code servira de clé dans le Dictionary
    // et de valeur unique dans le HashSet.

    public string Code
    {
        get
        {
            return code;
        }

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "Le code du produit ne peut pas être vide."
                );
            }

            code = value;
        }
    }


    // ------------------------------------------------------------------------
    // PROPRIÉTÉ Nom
    // ------------------------------------------------------------------------

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


    // ------------------------------------------------------------------------
    // PROPRIÉTÉ Prix
    // ------------------------------------------------------------------------

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


    // ------------------------------------------------------------------------
    // PROPRIÉTÉ Quantite
    // ------------------------------------------------------------------------

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


    // ------------------------------------------------------------------------
    // MÉTHODE COMMUNE
    // ------------------------------------------------------------------------

    public double ValeurStock()
    {
        return Prix * Quantite;
    }


    // ------------------------------------------------------------------------
    // MÉTHODE VIRTUAL
    // ------------------------------------------------------------------------
    //
    // Les sous-classes pourront redéfinir cette méthode.

    public virtual void Afficher()
    {
        Console.WriteLine($"Code            : {Code}");
        Console.WriteLine($"Nom             : {Nom}");
        Console.WriteLine($"Prix            : {Prix:F2} $");
        Console.WriteLine($"Quantité        : {Quantite}");
        Console.WriteLine(
            $"Valeur du stock : {ValeurStock():F2} $"
        );
    }


    // ------------------------------------------------------------------------
    // TOSTRING
    // ------------------------------------------------------------------------
    //
    // Permet d'obtenir un affichage utile lorsqu'on fait :
    //
    // Console.WriteLine(produit);

    public override string ToString()
    {
        return $"{Code} - {Nom} - {Prix:F2} $ - quantité : {Quantite}";
    }
}


// ============================================================================
// SOUS-CLASSE : ProduitElectronique
// ============================================================================

public class ProduitElectronique : Produit
{
    private int garantieMois;


    public ProduitElectronique(
        string code,
        string nom,
        double prix,
        int quantite,
        int garantieMois
    )
        : base(code, nom, prix, quantite)
    {
        if (garantieMois < 0)
        {
            throw new ArgumentException(
                "La garantie ne peut pas être négative."
            );
        }

        this.garantieMois = garantieMois;
    }


    public int GarantieMois
    {
        get
        {
            return garantieMois;
        }
    }


    public override void Afficher()
    {
        // Réutilisation de l'affichage commun.
        base.Afficher();

        // Information spécifique.
        Console.WriteLine("Type            : Électronique");
        Console.WriteLine(
            $"Garantie        : {GarantieMois} mois"
        );
    }
}


// ============================================================================
// SOUS-CLASSE : ProduitAlimentaire
// ============================================================================

public class ProduitAlimentaire : Produit
{
    private DateTime expiration;


    public ProduitAlimentaire(
        string code,
        string nom,
        double prix,
        int quantite,
        DateTime expiration
    )
        : base(code, nom, prix, quantite)
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
        base.Afficher();

        Console.WriteLine("Type            : Alimentaire");
        Console.WriteLine(
            $"Expiration      : {Expiration:yyyy-MM-dd}"
        );
    }
}


// ============================================================================
// SOUS-CLASSE : ProduitLivre
// ============================================================================

public class ProduitLivre : Produit
{
    private string auteur = string.Empty;


    public ProduitLivre(
        string code,
        string nom,
        double prix,
        int quantite,
        string auteur
    )
        : base(code, nom, prix, quantite)
    {
        if (string.IsNullOrWhiteSpace(auteur))
        {
            throw new ArgumentException(
                "L'auteur ne peut pas être vide."
            );
        }

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
        Console.WriteLine($"Auteur          : {Auteur}");
    }
}


// ============================================================================
// CLASSE GestionInventaire
// ============================================================================
//
// Cette classe montre que plusieurs collections peuvent travailler ensemble.
//
// List<Produit>
//     -> parcourir tous les produits.
//
// Dictionary<string, Produit>
//     -> retrouver rapidement un produit avec son code.
//
// HashSet<string>
//     -> empêcher les codes en doublon.
//
// Stack<Produit>
//     -> annuler le dernier ajout.
//
// Queue<Produit>
//     -> gérer une file de réapprovisionnement.
// ============================================================================

public class GestionInventaire
{
    // ------------------------------------------------------------------------
    // LIST
    // ------------------------------------------------------------------------

    private List<Produit> produits =
        new List<Produit>();


    // ------------------------------------------------------------------------
    // DICTIONARY
    // ------------------------------------------------------------------------

    private Dictionary<string, Produit> produitsParCode =
        new Dictionary<string, Produit>();


    // ------------------------------------------------------------------------
    // HASHSET
    // ------------------------------------------------------------------------

    private HashSet<string> codes =
        new HashSet<string>();


    // ------------------------------------------------------------------------
    // STACK
    // ------------------------------------------------------------------------

    private Stack<Produit> historiqueAjouts =
        new Stack<Produit>();


    // ------------------------------------------------------------------------
    // QUEUE
    // ------------------------------------------------------------------------

    private Queue<Produit> fileReapprovisionnement =
        new Queue<Produit>();


    // ------------------------------------------------------------------------
    // IEnumerable<Produit>
    // ------------------------------------------------------------------------
    //
    // Le programme extérieur peut parcourir les produits,
    // mais il ne reçoit pas directement la List privée.

    public IEnumerable<Produit> Produits
    {
        get
        {
            return produits;
        }
    }


    public IEnumerable<string> Codes
    {
        get
        {
            return codes;
        }
    }


    // ------------------------------------------------------------------------
    // AJOUTER UN PRODUIT
    // ------------------------------------------------------------------------

    public bool Ajouter(Produit produit)
    {
        /*
         * On commence par le HashSet.
         *
         * Add retourne :
         *
         * true  -> le code est nouveau
         * false -> le code existe déjà
         */

        bool codeNouveau = codes.Add(produit.Code);

        if (!codeNouveau)
        {
            return false;
        }


        // Le produit est ajouté à la List
        // pour pouvoir être parcouru.
        produits.Add(produit);


        // Le produit est ajouté au Dictionary
        // pour permettre la recherche par code.
        produitsParCode[produit.Code] = produit;


        // On mémorise le dernier ajout
        // pour pouvoir l'annuler.
        historiqueAjouts.Push(produit);


        return true;
    }


    // ------------------------------------------------------------------------
    // RECHERCHER AVEC DICTIONARY
    // ------------------------------------------------------------------------

    public Produit? ChercherParCode(string code)
    {
        /*
         * On vérifie la présence de la clé
         * avant d'utiliser produitsParCode[code].
         */

        if (produitsParCode.ContainsKey(code))
        {
            return produitsParCode[code];
        }

        return null;
    }


    // ------------------------------------------------------------------------
    // AFFICHER TOUS
    // ------------------------------------------------------------------------

    public void AfficherTous()
    {
        foreach (Produit produit in produits)
        {
            Console.WriteLine();
            Console.WriteLine("----------------------------------------");

            /*
             * Le tableau n'est pas nécessaire.
             *
             * La List<Produit> peut contenir :
             *
             * ProduitElectronique
             * ProduitAlimentaire
             * ProduitLivre
             *
             * Grâce au polymorphisme.
             */

            produit.Afficher();
        }
    }


    // ------------------------------------------------------------------------
    // FILTRER LES PRODUITS
    // ------------------------------------------------------------------------

    public void AfficherStockFaible(int seuil)
    {
        foreach (Produit produit in produits)
        {
            // foreach visite tous les produits.
            //
            // if décide lesquels afficher.

            if (produit.Quantite <= seuil)
            {
                Console.WriteLine(produit);
            }
        }
    }


    // ------------------------------------------------------------------------
    // STACK : ANNULER LE DERNIER AJOUT
    // ------------------------------------------------------------------------

    public Produit? AnnulerDernierAjout()
    {
        if (historiqueAjouts.Count == 0)
        {
            return null;
        }


        // Pop retire le dernier produit ajouté.
        Produit produit = historiqueAjouts.Pop();


        // Important :
        // il faut supprimer ce produit de toutes
        // les collections principales.

        produits.Remove(produit);

        produitsParCode.Remove(produit.Code);

        codes.Remove(produit.Code);


        return produit;
    }


    // ------------------------------------------------------------------------
    // QUEUE : AJOUTER À LA FILE DE RÉAPPROVISIONNEMENT
    // ------------------------------------------------------------------------

    public bool AjouterAFileReapprovisionnement(string code)
    {
        Produit? produit = ChercherParCode(code);


        if (produit == null)
        {
            return false;
        }


        // Enqueue ajoute à la fin de la file.
        fileReapprovisionnement.Enqueue(produit);


        return true;
    }


    // ------------------------------------------------------------------------
    // QUEUE : TRAITER LE PREMIER PRODUIT
    // ------------------------------------------------------------------------

    public Produit? TraiterProchainReapprovisionnement()
    {
        if (fileReapprovisionnement.Count == 0)
        {
            return null;
        }


        // Dequeue retire le premier élément.
        return fileReapprovisionnement.Dequeue();
    }
}


// ============================================================================
// PROGRAMME PRINCIPAL
// ============================================================================

public class Program
{
    public static void Main(string[] args)
    {
        Titre("LABO - COLLECTIONS ET GÉNÉRIQUES");


        GestionInventaire inventaire =
            new GestionInventaire();


        // ====================================================================
        // TEST 1 - CRÉATION DES PRODUITS
        // ====================================================================

        Titre("TEST 1 - Création des produits");


        ProduitElectronique ecouteurs =
            new ProduitElectronique(
                "E001",
                "Écouteurs",
                79.99,
                8,
                24
            );


        ProduitAlimentaire yogourt =
            new ProduitAlimentaire(
                "A001",
                "Yogourt",
                3.99,
                3,
                new DateTime(2026, 9, 15)
            );


        ProduitLivre livre =
            new ProduitLivre(
                "L001",
                "Clean Code",
                49.95,
                2,
                "Robert C. Martin"
            );


        ProduitElectronique clavier =
            new ProduitElectronique(
                "E002",
                "Clavier",
                129.99,
                12,
                36
            );


        Console.WriteLine(
            $"Nombre d'objets Produit créés : {Produit.NbProduits}"
        );


        // ====================================================================
        // TEST 2 - AJOUT DANS LES COLLECTIONS
        // ====================================================================

        Titre("TEST 2 - Ajouter les produits");


        AjouterEtAfficherResultat(
            inventaire,
            ecouteurs
        );


        AjouterEtAfficherResultat(
            inventaire,
            yogourt
        );


        AjouterEtAfficherResultat(
            inventaire,
            livre
        );


        AjouterEtAfficherResultat(
            inventaire,
            clavier
        );


        // ====================================================================
        // TEST 3 - HASHSET ET DOUBLON
        // ====================================================================

        Titre("TEST 3 - Refuser un doublon");


        bool ajouteEncore =
            inventaire.Ajouter(ecouteurs);


        if (ajouteEncore)
        {
            Console.WriteLine(
                "Le produit a été ajouté."
            );
        }
        else
        {
            Console.WriteLine(
                "Ajout refusé : le code E001 existe déjà."
            );
        }


        /*
         * Le HashSet protège ici l'unicité.
         *
         * E001 existe déjà.
         *
         * codes.Add("E001")
         *
         * retourne donc false.
         */


        // ====================================================================
        // TEST 4 - LIST + FOREACH + POLYMORPHISME
        // ====================================================================

        Titre("TEST 4 - Parcourir la List");


        inventaire.AfficherTous();


        /*
         * List<Produit> contient des objets
         * de types différents.
         *
         * Pourtant, le foreach utilise toujours :
         *
         * produit.Afficher();
         *
         * ProduitElectronique -> version électronique
         * ProduitAlimentaire  -> version alimentaire
         * ProduitLivre        -> version livre
         */


        // ====================================================================
        // TEST 5 - DICTIONARY
        // ====================================================================

        Titre("TEST 5 - Chercher par code");


        Produit? trouve =
            inventaire.ChercherParCode("A001");


        if (trouve != null)
        {
            Console.WriteLine(
                "Produit trouvé :"
            );

            Console.WriteLine(trouve);
        }
        else
        {
            Console.WriteLine(
                "Produit absent."
            );
        }


        // ====================================================================
        // TEST 6 - CLÉ ABSENTE
        // ====================================================================

        Titre("TEST 6 - Chercher un code absent");


        Produit? absent =
            inventaire.ChercherParCode("Z999");


        if (absent == null)
        {
            Console.WriteLine(
                "Le code Z999 n'existe pas."
            );
        }


        /*
         * Contre-cas :
         *
         * produitsParCode["Z999"]
         *
         * directement sans vérifier.
         *
         * Cela provoquerait une erreur
         * si la clé n'existe pas.
         */


        // ====================================================================
        // TEST 7 - FILTRE
        // ====================================================================

        Titre("TEST 7 - Stock faible");


        Console.WriteLine(
            "Produits avec quantité <= 3 :"
        );


        inventaire.AfficherStockFaible(3);


        // ====================================================================
        // TEST 8 - MÉTHODE GÉNÉRIQUE
        // ====================================================================

        Titre("TEST 8 - Méthode générique");


        Console.WriteLine(
            "Afficher les produits :"
        );


        AfficherTous(
            inventaire.Produits
        );


        Console.WriteLine();


        Console.WriteLine(
            "Afficher les codes :"
        );


        AfficherTous(
            inventaire.Codes
        );


        /*
         * Même méthode :
         *
         * AfficherTous<T>
         *
         * mais T peut être :
         *
         * Produit
         *
         * ou
         *
         * string
         */


        // ====================================================================
        // TEST 9 - STACK
        // ====================================================================

        Titre("TEST 9 - Stack : annuler");


        Produit? produitAnnule =
            inventaire.AnnulerDernierAjout();


        if (produitAnnule != null)
        {
            Console.WriteLine(
                $"Dernier ajout annulé : {produitAnnule.Code} - {produitAnnule.Nom}"
            );
        }


        Console.WriteLine();


        Console.WriteLine(
            "Inventaire après annulation :"
        );


        AfficherTous(
            inventaire.Produits
        );


        /*
         * Le dernier produit ajouté était :
         *
         * E002 - Clavier
         *
         * Stack fonctionne en LIFO :
         *
         * Last In
         * First Out
         */


        // ====================================================================
        // TEST 10 - VÉRIFIER LE DICTIONARY APRÈS L'ANNULATION
        // ====================================================================

        Titre("TEST 10 - Collections cohérentes");


        Produit? rechercheClavier =
            inventaire.ChercherParCode("E002");


        if (rechercheClavier == null)
        {
            Console.WriteLine(
                "E002 a aussi été retiré du Dictionary."
            );
        }


        // ====================================================================
        // TEST 11 - QUEUE
        // ====================================================================

        Titre("TEST 11 - Queue : réapprovisionnement");


        inventaire.AjouterAFileReapprovisionnement(
            "A001"
        );

        inventaire.AjouterAFileReapprovisionnement(
            "L001"
        );

        inventaire.AjouterAFileReapprovisionnement(
            "E001"
        );


        Produit? premier =
            inventaire.TraiterProchainReapprovisionnement();


        if (premier != null)
        {
            Console.WriteLine(
                $"Premier produit traité : {premier.Code} - {premier.Nom}"
            );
        }


        Produit? deuxieme =
            inventaire.TraiterProchainReapprovisionnement();


        if (deuxieme != null)
        {
            Console.WriteLine(
                $"Deuxième produit traité : {deuxieme.Code} - {deuxieme.Nom}"
            );
        }


        /*
         * Ordre d'entrée :
         *
         * A001
         * L001
         * E001
         *
         * Ordre de sortie :
         *
         * A001
         * L001
         * E001
         *
         * Queue fonctionne en FIFO :
         *
         * First In
         * First Out
         */


        // ====================================================================
        // TEST 12 - AJOUT D'UN AUTRE TYPE
        // ====================================================================

        Titre("TEST 12 - Collections + polymorphisme");


        ProduitLivre nouveauLivre =
            new ProduitLivre(
                "L002",
                "C# pour débutants",
                39.99,
                6,
                "Marie Tremblay"
            );


        inventaire.Ajouter(
            nouveauLivre
        );


        Console.WriteLine(
            "Un nouveau ProduitLivre a été ajouté."
        );

        Console.WriteLine(
            "Aucune nouvelle List n'a été nécessaire."
        );


        inventaire.AfficherTous();


        // ====================================================================
        // FIN
        // ====================================================================

        Titre("FIN DU LABO");
    }


    // ========================================================================
    // MÉTHODE GÉNÉRIQUE
    // ========================================================================
    //
    // T représente le type des éléments.
    //
    // IEnumerable<T> signifie simplement :
    //
    // "Je veux quelque chose que je peux parcourir."
    //
    // La méthode n'a pas besoin de savoir
    // s'il s'agit d'une List, d'un HashSet, etc.

    public static void AfficherTous<T>(
        IEnumerable<T> elements
    )
    {
        foreach (T element in elements)
        {
            Console.WriteLine(element);
        }
    }


    // ========================================================================
    // MÉTHODE POUR TESTER L'AJOUT
    // ========================================================================

    public static void AjouterEtAfficherResultat(
        GestionInventaire inventaire,
        Produit produit
    )
    {
        bool ajoute =
            inventaire.Ajouter(produit);


        if (ajoute)
        {
            Console.WriteLine(
                $"Ajouté : {produit.Code} - {produit.Nom}"
            );
        }
        else
        {
            Console.WriteLine(
                $"Refusé : le code {produit.Code} existe déjà."
            );
        }
    }


    // ========================================================================
    // MÉTHODE POUR RENDRE LA CONSOLE PLUS LISIBLE
    // ========================================================================

    public static void Titre(string texte)
    {
        Console.WriteLine();

        Console.WriteLine(
            "============================================================"
        );

        Console.WriteLine(texte);

        Console.WriteLine(
            "============================================================"
        );
    }
}