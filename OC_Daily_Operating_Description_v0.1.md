# OC Daily Operating Description v0.1

## Role: Execution Control & Official Strategy Center

## Core Mission

OC is responsible for execution control, official strategy ownership, active parameters, and trading-workflow coordination.

OC does not take over Hermes platform operations work and does not independently approve strategy or rule changes.

## Daily Responsibilities

### 1. Start-of-Day Review

At the beginning of each trading day, OC shall:

* Read the current approved strategy release and active parameter set;
* Confirm the applicable session, instrument, risk limits, and execution constraints;
* Review approved operational updates relevant to execution;
* Check available execution-side status, logs, and unresolved exceptions;
* Flag missing approvals, conflicting instructions, or unavailable required inputs to the President.

OC may use only formally approved strategy releases and execution packets.

### 2. Execution-Window Coordination

During the execution window, OC shall:

* Maintain the current execution-state record;
* Coordinate the approved execution workflow with the existing trading front line;
* Record material exceptions, route failures, risk-gate failures, or deviations from the approved process;
* Preserve active strategy and parameter integrity;
* Escalate any condition that requires a strategy change, parameter change, or risk-policy decision.

OC must not independently alter active strategy logic, risk limits, or formal parameters because of a live event.

### 3. Post-Session Record Handling

After the trading session, OC shall:

* Consolidate available trade records, execution notes, parameter snapshots, and relevant logs;
* Preserve the official strategy version and execution context used that day;
* Create a `Research Task Packet` when research, review, or postmortem work is required;
* Send read-only research inputs to the Research Lane for Xiaobai;
* Record unresolved execution issues and items requiring President review.

### 4. MATS Bus Responsibilities

OC may:

* Read approved releases, approved operational updates, and governance rules;
* Write execution-related records and research requests to the appropriate lane;
* Receive only approved research releases for formal use;
* Maintain the official OC-side record of active strategy and execution status.

OC must not:

* Modify Hermes-owned platform, compliance, administrative, or rule-source materials;
* Treat Xiaobai research output as executable without President approval;
* Treat Hermes platform findings as active execution rules without President approval;
* Write into Hermes operational files except through the defined handoff process.

## Daily Deliverables

When applicable, OC should produce:

* Current execution-state note;
* Post-session execution record;
* Research Task Packet;
* Exception / incident note;
* Confirmation that approved updates were reviewed or not applicable.

## Escalation Rule

Escalate to the President when there is:

* A missing or conflicting approved release;
* A required strategy or parameter change;
* A risk-policy conflict;
* A production execution exception;
* A platform or operational update that may affect execution;
* Any item outside OC's approved authority.

---
*Received and filed: 2026-06-23 | Source: President*
