﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sharpliner.AzureDevOps.ConditionedExpressions;

namespace Sharpliner.AzureDevOps;

/// <summary>
/// Extension methods for creating conditioned definitions.
/// </summary>
public static class ConditionExtensions
{
    /// <summary>
    /// Defines a variable.
    /// </summary>
    /// <param name="condition">Conditioned definition</param>
    /// <param name="name">Variable name</param>
    /// <param name="value">Variable value</param>
    public static Conditioned<VariableBase> Variable(this IfCondition condition, string name, string value)
        => Conditioned.Link<VariableBase>(condition, new Variable(name, value));

    /// <summary>
    /// Defines a variable.
    /// </summary>
    /// <param name="condition">Conditioned definition</param>
    /// <param name="name">Variable name</param>
    /// <param name="value">Variable value</param>
    public static Conditioned<VariableBase> Variable(this IfCondition condition, string name, bool value)
        => Conditioned.Link<VariableBase>(condition, new Variable(name, value));

    /// <summary>
    /// Defines a variable.
    /// </summary>
    /// <param name="condition">Conditioned definition</param>
    /// <param name="name">Variable name</param>
    /// <param name="value">Variable value</param>
    public static Conditioned<VariableBase> Variable(this IfCondition condition, string name, int value)
        => Conditioned.Link<VariableBase>(condition, new Variable(name, value));

    /// <summary>
    /// Defines a variable.
    /// </summary>
    /// <param name="condition">Conditioned definition</param>
    /// <param name="name">Variable name</param>
    /// <param name="value">Variable value</param>
    public static Conditioned<VariableBase> Variable(this IfCondition condition, string name, Enum value)
        => Conditioned.Link<VariableBase>(condition, new Variable(name, value));

    /// <summary>
    /// Defines a variable.
    /// </summary>
    /// <param name="condition">Conditioned definition</param>
    /// <param name="variable">Variable instance</param>
    public static Conditioned<VariableBase> Variable(this IfCondition condition, Variable variable)
        => Conditioned.Link<VariableBase>(condition, variable);

    /// <summary>
    /// Defines multiple variables at once.
    /// </summary>
    /// <param name="condition">Conditioned definition</param>
    /// <param name="variables">List of (key, value) pairs</param>
    public static Conditioned<VariableBase> Variables(
        this IfCondition condition,
        params (string name, object value)[] variables)
    {
        var (name, value) = variables.First();
        var conditionedDefinition = value switch
        {
            int number => Conditioned.Link<VariableBase>(condition, new Variable(name, number)),
            bool boolean => Conditioned.Link<VariableBase>(condition, new Variable(name, boolean)),
            string s => Conditioned.Link<VariableBase>(condition, new Variable(name, s)),
            object any => Conditioned.Link<VariableBase>(condition, new Variable(name, any?.ToString() ?? string.Empty)),
        };

        if (variables.Length > 1)
        {
            conditionedDefinition = conditionedDefinition.Variables(variables.Skip(1).ToArray());
        }

        return conditionedDefinition;
    }

    /// <summary>
    /// References a variable group.
    /// </summary>
    public static Conditioned<VariableBase> Group(this IfCondition condition, string name)
        => Conditioned.Link<VariableBase>(condition, new VariableGroup(name));

    /// <summary>
    /// Creates a new stage.
    /// </summary>
    public static Conditioned<Stage> Stage(this IfCondition condition, Stage stage)
        => Conditioned.Link(condition, stage);

    /// <summary>
    /// Creates a new stage.
    /// </summary>
    public static Conditioned<Stage> Stage(this IfCondition condition, Conditioned<Stage> stage)
        => Conditioned.Link(condition, stage);

    /// <summary>
    /// Creates a new job.
    /// </summary>
    public static Conditioned<JobBase> Job(this IfCondition condition, JobBase job)
        => Conditioned.Link(condition, job);

    /// <summary>
    /// Creates a new job.
    /// </summary>
    public static Conditioned<JobBase> Job(this IfCondition condition, Conditioned<JobBase> job)
        => Conditioned.Link(condition, job);

    /// <summary>
    /// Creates a new deployment job.
    /// </summary>
    public static Conditioned<JobBase> DeploymentJob(this IfCondition condition, JobBase job)
        => Conditioned.Link(condition, job);

    /// <summary>
    /// Creates a new deployment job.
    /// </summary>
    public static Conditioned<JobBase> DeploymentJob(this IfCondition condition, Conditioned<JobBase> job)
        => Conditioned.Link(condition, job);

    /// <summary>
    /// Creates a new step.
    /// </summary>
    public static Conditioned<Step> Step(this IfCondition condition, Step step)
        => Conditioned.Link(condition, step);

    /// <summary>
    /// Creates a new step.
    /// </summary>
    public static Conditioned<Step> Step(this IfCondition condition, Conditioned<Step> step)
        => Conditioned.Link(condition, step);

    /// <summary>
    /// Creates a new pool.
    /// </summary>
    public static Conditioned<Pool> Pool(this IfCondition condition, Pool pool)
        => Conditioned.Link(condition, pool);

    /// <summary>
    /// Creates a new pool.
    /// </summary>
    public static Conditioned<Pool> Pool(this IfCondition condition, Conditioned<Pool> pool)
        => Conditioned.Link(condition, pool);

    /// <summary>
    /// Reference a YAML stage template.
    /// </summary>
    /// <param name="condition">Conditioned definition</param>
    /// <param name="path">Relative path to the YAML file with the template</param>
    /// <param name="parameters">Values for template parameters</param>
    public static Conditioned<Stage> StageTemplate(this IfCondition condition, string path, TemplateParameters? parameters = null)
        => Conditioned.Link(condition, new Template<Stage>(condition: condition, path: path, parameters ?? []));

    /// <summary>
    /// Reference a YAML job template.
    /// </summary>
    /// <param name="condition">Conditioned definition</param>
    /// <param name="path">Relative path to the YAML file with the template</param>
    /// <param name="parameters">Values for template parameters</param>
    public static Conditioned<JobBase> JobTemplate(this IfCondition condition, string path, TemplateParameters? parameters = null)
        => Conditioned.Link(condition, new Template<JobBase>(condition: condition, path: path, parameters ?? []));

    /// <summary>
    /// Reference a YAML step template.
    /// </summary>
    /// <param name="condition">Conditioned definition</param>
    /// <param name="path">Relative path to the YAML file with the template</param>
    /// <param name="parameters">Values for template parameters</param>
    public static Conditioned<Step> StepTemplate(this IfCondition condition, string path, TemplateParameters? parameters = null)
        => Conditioned.Link(condition, new Template<Step>(condition: condition, path: path, parameters ?? []));

    /// <summary>
    /// Reference a YAML variable template.
    /// </summary>
    /// <param name="condition">Conditioned definition</param>
    /// <param name="path">Relative path to the YAML file with the template</param>
    /// <param name="parameters">Values for template parameters</param>
    public static Conditioned<VariableBase> VariableTemplate(this IfCondition condition, string path, TemplateParameters? parameters = null)
        => Conditioned.Link(condition, new Template<VariableBase>(condition: condition, path: path, parameters ?? []));

    /// <summary>
    /// Reference a stage library (series of library stages).
    /// </summary>
    public static Conditioned<Stage> StageLibrary(this IfCondition condition, StageLibrary library)
        => Conditioned.Link(condition, library.Items);

    /// <summary>
    /// Reference a job library (series of library jobs).
    /// </summary>
    public static Conditioned<JobBase> JobLibrary(this IfCondition condition, JobLibrary library)
        => Conditioned.Link(condition, library.Items);

    /// <summary>
    /// Reference a step library (series of library steps).
    /// </summary>
    public static Conditioned<Step> StepLibrary(this IfCondition condition, StepLibrary library)
        => Conditioned.Link(condition, library.Items);

    /// <summary>
    /// Reference a variable library (series of library Variables).
    /// </summary>
    public static Conditioned<VariableBase> VariableLibrary(this IfCondition condition, VariableLibrary library)
        => Conditioned.Link(condition, library.Items);

    /// <summary>
    /// Reference a step library (series of library stages).
    /// </summary>
    public static Conditioned<Stage> StageLibrary<T>(this IfCondition condition) where T : StageLibrary, new()
        => Conditioned.Link(condition, new T().Items);

    /// <summary>
    /// Reference a step library (series of library jobs).
    /// </summary>
    public static Conditioned<JobBase> JobLibrary<T>(this IfCondition condition) where T : JobLibrary, new()
        => Conditioned.Link(condition, new T().Items);

    /// <summary>
    /// Reference a step library (series of library steps).
    /// </summary>
    public static Conditioned<Step> StepLibrary<T>(this IfCondition condition) where T : StepLibrary, new()
        => Conditioned.Link(condition, new T().Items);

    /// <summary>
    /// Reference a step library (series of library Variables).
    /// </summary>
    public static Conditioned<VariableBase> VariableLibrary<T>(this IfCondition condition) where T : VariableLibrary, new()
        => Conditioned.Link(condition, new T().Items);

    /// <summary>
    /// Include a set of stages.
    /// </summary>
    public static Conditioned<Stage> Stages(this IfCondition condition, params Conditioned<Stage>[] stages)
        => Conditioned.Link<Stage>(condition, stages);

    /// <summary>
    /// Include a set of jobs.
    /// </summary>
    public static Conditioned<Job> Jobs(this IfCondition condition, params Conditioned<Job>[] jobs)
        => Conditioned.Link<Job>(condition, jobs);

    /// <summary>
    /// Include a set of steps.
    /// </summary>
    public static Conditioned<Step> Steps(this IfCondition condition, params Conditioned<Step>[] steps)
        => Conditioned.Link<Step>(condition, steps);

    /// <summary>
    /// Include a set of variables.
    /// </summary>
    public static Conditioned<VariableBase> Variables(this IfCondition condition, params Conditioned<VariableBase>[] variables)
        => Conditioned.Link<VariableBase>(condition, variables);

    /// <summary>
    /// Include a set of stages.
    /// </summary>
    public static Conditioned<Stage> Stages(this IfCondition condition, IEnumerable<Conditioned<Stage>> stages)
        => Conditioned.Link(condition, stages);

    /// <summary>
    /// Include a set of jobs.
    /// </summary>
    public static Conditioned<Job> Jobs(this IfCondition condition, IEnumerable<Conditioned<Job>> jobs)
        => Conditioned.Link(condition, jobs);

    /// <summary>
    /// Include a set of steps.
    /// </summary>
    public static Conditioned<Step> Steps(this IfCondition condition, IEnumerable<Conditioned<Step>> steps)
        => Conditioned.Link(condition, steps);

    /// <summary>
    /// Include a set of variables.
    /// </summary>
    public static Conditioned<VariableBase> Variables(this IfCondition condition, IEnumerable<Conditioned<VariableBase>> variables)
        => Conditioned.Link(condition, variables);

    /// <summary>
    /// Include a set of stages.
    /// </summary>
    public static Conditioned<Stage> Stages(this IfCondition condition, params Stage[] stages)
        => Conditioned.Link(condition, stages.Select(x => new Conditioned<Stage>(x)));

    /// <summary>
    /// Include a set of jobs.
    /// </summary>
    public static Conditioned<Job> Jobs(this IfCondition condition, params Job[] jobs)
        => Conditioned.Link(condition, jobs.Select(x => new Conditioned<Job>(x)));

    /// <summary>
    /// Include a set of steps.
    /// </summary>
    public static Conditioned<Step> Steps(this IfCondition condition, params Step[] steps)
        => Conditioned.Link(condition, steps.Select(x => new Conditioned<Step>(x)));

    /// <summary>
    /// Include a set of variables.
    /// </summary>
    public static Conditioned<VariableBase> Variables(this IfCondition condition, params VariableBase[] variables)
        => Conditioned.Link(condition, variables.Select(x => new Conditioned<VariableBase>(x)));

    /// <summary>
    /// Include a set of stages.
    /// </summary>
    public static Conditioned<Stage> Stages(this IfCondition condition, IEnumerable<Stage> stages)
        => condition.Stages(stages.ToArray());

    /// <summary>
    /// Include a set of jobs.
    /// </summary>
    public static Conditioned<Job> Jobs(this IfCondition condition, IEnumerable<Job> jobs)
        => condition.Jobs(jobs.ToArray());

    /// <summary>
    /// Include a set of steps.
    /// </summary>
    public static Conditioned<Step> Steps(this IfCondition condition, IEnumerable<Step> steps)
        => condition.Steps(steps.ToArray());

    /// <summary>
    /// Include a set of variables.
    /// </summary>
    public static Conditioned<VariableBase> Variables(this IfCondition condition, IEnumerable<VariableBase> variables)
        => condition.Variables(variables.ToArray());

    /// <summary>
    /// Includes a strategy.
    /// </summary>
    public static Conditioned<Strategy> Strategy(this IfCondition condition, Strategy strategy)
        => Conditioned.Link(condition, strategy);

    /// <summary>
    /// Includes a generic object.
    /// </summary>
    public static Conditioned<T> Value<T>(this IfCondition condition, T value)
        => Conditioned.Link(condition, value);

    /// <summary>
    /// Starts an <c>${{ each var in collection }}</c> section.
    /// For example:
    /// <code lang="csharp">
    /// If.IsBranch("main")
    ///     .Each("env", "parameters.environments")
    ///         .Stage(new Stage("stage-${{ env.name }}")
    ///         {
    ///         })
    /// </code>
    /// will generate:
    /// <code lang="yaml">
    /// - ${{ if eq(variables['Build.SourceBranch'], 'refs/heads/main') }}:
    ///   - ${{ each env in parameters.stages }}:
    ///     - stage: stage-${{ env.name }}
    /// </code>
    /// </summary>
    /// <param name="condition">The current condition.</param>
    /// <param name="iterator">Name of the iterator variable.</param>
    /// <param name="collection">Collection to iterate over.</param>
    /// <returns>An if condition with an <c>each</c> expression appended.</returns>
    public static IfCondition Each(this IfCondition condition, string iterator, string collection)
    {
        condition.EachExpression = new(iterator, collection);
        return condition;
    }
}
