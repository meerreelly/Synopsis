﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository.Domain;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ActorFilm", b =>
                {
                    b.Property<int>("ActorsActorId")
                        .HasColumnType("integer");

                    b.Property<int>("FilmsFilmId")
                        .HasColumnType("integer");

                    b.HasKey("ActorsActorId", "FilmsFilmId");

                    b.HasIndex("FilmsFilmId");

                    b.ToTable("ActorFilm");
                });

            modelBuilder.Entity("Core.Actor", b =>
                {
                    b.Property<int>("ActorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ActorId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.HasKey("ActorId");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("Core.Director", b =>
                {
                    b.Property<int>("DirectorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DirectorId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.HasKey("DirectorId");

                    b.ToTable("Directors");
                });

            modelBuilder.Entity("Core.Film", b =>
                {
                    b.Property<int>("FilmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FilmId"));

                    b.Property<string>("Certificate")
                        .HasColumnType("text");

                    b.Property<double>("Gross")
                        .HasColumnType("double precision");

                    b.Property<double>("ImdbRating")
                        .HasColumnType("double precision");

                    b.Property<int>("MetaScore")
                        .HasColumnType("integer");

                    b.Property<int>("NumberOfVotes")
                        .HasColumnType("integer");

                    b.Property<string>("PosterUrl")
                        .HasColumnType("text");

                    b.Property<int>("ReleasedYear")
                        .HasColumnType("integer");

                    b.Property<int>("Runtime")
                        .HasColumnType("integer");

                    b.Property<string>("ShortStory")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<int>("TitleTypeId")
                        .HasColumnType("integer");

                    b.HasKey("FilmId");

                    b.HasIndex("TitleTypeId");

                    b.ToTable("Films");
                });

            modelBuilder.Entity("Core.FilmLink", b =>
                {
                    b.Property<int>("FilmLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FilmLinkId"));

                    b.Property<int>("FilmId")
                        .HasColumnType("integer");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("FilmLinkId");

                    b.HasIndex("FilmId");

                    b.ToTable("FilmLinks");
                });

            modelBuilder.Entity("Core.FilmPoint", b =>
                {
                    b.Property<int>("FilmPointId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FilmPointId"));

                    b.Property<int>("FilmId")
                        .HasColumnType("integer");

                    b.Property<int>("Point")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("FilmPointId");

                    b.HasIndex("FilmId");

                    b.HasIndex("UserId");

                    b.ToTable("FilmPoints");
                });

            modelBuilder.Entity("Core.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GenreId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Core.Overview", b =>
                {
                    b.Property<int>("OverviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OverviewId"));

                    b.Property<int>("FilmId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("UserOverview")
                        .HasColumnType("text");

                    b.HasKey("OverviewId");

                    b.HasIndex("FilmId");

                    b.HasIndex("UserId");

                    b.ToTable("Overviews");
                });

            modelBuilder.Entity("Core.TitleType", b =>
                {
                    b.Property<int>("TitleTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TitleTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TitleTypeId");

                    b.ToTable("TitleTypes");
                });

            modelBuilder.Entity("Core.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("text");

                    b.Property<string>("DiscordId")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GoogleId")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Core.ViewStatus", b =>
                {
                    b.Property<int>("ViewStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ViewStatusId"));

                    b.Property<int>("FilmId")
                        .HasColumnType("integer");

                    b.Property<int?>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("ViewStatusId");

                    b.HasIndex("FilmId");

                    b.HasIndex("UserId");

                    b.ToTable("ViewStatuses");
                });

            modelBuilder.Entity("DirectorFilm", b =>
                {
                    b.Property<int>("DirectorsDirectorId")
                        .HasColumnType("integer");

                    b.Property<int>("FilmsFilmId")
                        .HasColumnType("integer");

                    b.HasKey("DirectorsDirectorId", "FilmsFilmId");

                    b.HasIndex("FilmsFilmId");

                    b.ToTable("DirectorFilm");
                });

            modelBuilder.Entity("FilmGenre", b =>
                {
                    b.Property<int>("FilmsFilmId")
                        .HasColumnType("integer");

                    b.Property<int>("GenresGenreId")
                        .HasColumnType("integer");

                    b.HasKey("FilmsFilmId", "GenresGenreId");

                    b.HasIndex("GenresGenreId");

                    b.ToTable("FilmGenre");
                });

            modelBuilder.Entity("ActorFilm", b =>
                {
                    b.HasOne("Core.Actor", null)
                        .WithMany()
                        .HasForeignKey("ActorsActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Film", null)
                        .WithMany()
                        .HasForeignKey("FilmsFilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Film", b =>
                {
                    b.HasOne("Core.TitleType", "TitleType")
                        .WithMany()
                        .HasForeignKey("TitleTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TitleType");
                });

            modelBuilder.Entity("Core.FilmLink", b =>
                {
                    b.HasOne("Core.Film", "Film")
                        .WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");
                });

            modelBuilder.Entity("Core.FilmPoint", b =>
                {
                    b.HasOne("Core.Film", "Film")
                        .WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.User", "User")
                        .WithMany("FilmPoints")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Overview", b =>
                {
                    b.HasOne("Core.Film", "Film")
                        .WithMany("Overviews")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.User", "User")
                        .WithMany("Overviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.ViewStatus", b =>
                {
                    b.HasOne("Core.Film", "Film")
                        .WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.User", "User")
                        .WithMany("ViewStatuses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DirectorFilm", b =>
                {
                    b.HasOne("Core.Director", null)
                        .WithMany()
                        .HasForeignKey("DirectorsDirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Film", null)
                        .WithMany()
                        .HasForeignKey("FilmsFilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FilmGenre", b =>
                {
                    b.HasOne("Core.Film", null)
                        .WithMany()
                        .HasForeignKey("FilmsFilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Film", b =>
                {
                    b.Navigation("Overviews");
                });

            modelBuilder.Entity("Core.User", b =>
                {
                    b.Navigation("FilmPoints");

                    b.Navigation("Overviews");

                    b.Navigation("ViewStatuses");
                });
#pragma warning restore 612, 618
        }
    }
}
