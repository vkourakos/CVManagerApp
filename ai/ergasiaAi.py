import networkx as nx
import random
import matplotlib.pyplot as plt

graph = {1: [2, 3, 12],
         2: [1, 3, 4, 11, 12],
         3: [1, 2, 4, 5, 6, 7],
         4: [2, 3, 7, 9],
         5: [3, 6],
         6: [3, 5, 7],
         7: [3, 4, 6, 8, 9],
         8: [7, 9, 10],
         9: [4, 7, 8, 10, 11],
         10: [8, 9, 11, 13],
         11: [2, 9, 10, 12, 13],
         12: [1, 2, 11, 13],
         13: [10, 11, 12]
         }

mutation_rate = 0.1
elitism_rate = 0.3
num_colors = 4

def initpop(population_size):
    population = []
    for i in range(population_size):
        individual = []
        for j in range(len(graph)):
            color = random.randint(1, num_colors)
            individual.append(color)
        population.append(individual)
    return population

def fitness_function(individual):
    score = 0
    for node in graph:
        for neighbor in graph[node]:
            if individual[node-1] != individual[neighbor-1]:
                score += 1
    return score


def crossover(parent1, parent2):
    crossover_point = len(parent1)//2
    child1 = parent1[:crossover_point] + parent2[crossover_point:]
    child2 = parent1[crossover_point:] + parent2[:crossover_point]
    return child1, child2


def mutate(individual):
    mutated = individual[:]
    for i in range(len(individual)):
        if random.random() <= mutation_rate:
            color_choices = [c for c in range(1, num_colors + 1) if c != mutated[i]]
            mutated[i] = random.choice(color_choices)
    return mutated


def select_parents(population):
    fitness_scores = []
    for individual in population:
        fitness_scores.append(fitness_function(individual))
    
    total_score = sum(fitness_scores)

    probabilities = []
    for score in fitness_scores:
        probabilities.append(score / total_score)

    parent1 = random.choices(population, weights=probabilities)[0]
    parent2 = random.choices(population, weights=probabilities)[0]
    while parent2 == parent1:
        parent2 = random.choices(population, weights=probabilities)[0]

    return parent1, parent2

def draw_graph(graph):
    G = nx.Graph(graph)
    pos = nx.spring_layout(G)
    colors = [color_map[best_individual[i]] for i in range(len(graph))]
    print(colors)
    nx.draw(G, pos, node_color=colors, with_labels=True)
    plt.show()

def run_genetic_algorithm(population_size, num_generations):
   # Ο initial_population είναι μια λίστα με λίστες. Κάθε εμφωλευμένη λίστα αναπαριστά μια υποψήφια λύση
    initial_population = initpop(population_size)

    for generation in range(num_generations):
        # Ο πληθυσμός μετά από κάθε γενιά
        new_population = []

        for i in range(population_size):
            parent1, parent2 = select_parents(initial_population)
            # Crossover γονέων
            child1, child2 = crossover(parent1, parent2)
            # Μετάλλαξη των παιδιών
            child1 = mutate(child1)
            child2 = mutate(child2)

            new_population.append(child1)
            new_population.append(child2)
        #   Elitism (Κρατάω ένα ποσοστό elite γονέων, το ποσοστό θα είναι το population size * το rate 0.3)
        num_elites = int(population_size * elitism_rate)
        #   Παίρνω τους elite γονείς από τον προηγούμενο πληθυσμό και αντικαθιστούν κάποιες από τις λύσεις του νέου πληθυσμού
        elites = sorted(initial_population, key=fitness_function, reverse=True)[:num_elites]
        new_population[:num_elites] = elites

        initial_population = new_population 

    return max(initial_population, key=fitness_function)

color_map = {1: 'red', 2: 'green', 3: 'blue', 4: 'yellow'}
best_individual = run_genetic_algorithm(population_size=100, num_generations=200)
print("Best individual:", best_individual)
print("Fitness score:", fitness_function(best_individual))
draw_graph(graph)